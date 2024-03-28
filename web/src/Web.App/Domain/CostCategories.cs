using System.Collections.ObjectModel;

namespace Web.App.Domain;

public abstract class Category(decimal actual, SchoolExpenditure expenditure)
{
    public abstract decimal Value { get; }
    public decimal Actual => actual;
    public decimal PercentageExpenditure => decimal.Round(Actual / expenditure.TotalExpenditure * 100, 2, MidpointRounding.AwayFromZero);
    public decimal PercentageIncome => decimal.Round(Actual / expenditure.TotalExpenditure * 100, 2, MidpointRounding.AwayFromZero);
}

public class PupilCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => decimal.Round(_actual / _expenditure.NumberOfPupils, 0, MidpointRounding.AwayFromZero);
}

public class AreaCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => decimal.Round(_actual / _expenditure.FloorArea, 0, MidpointRounding.AwayFromZero);
}


public abstract class CostCategory
{
    private readonly Dictionary<string, Category> _values = new();

    protected Category this[string index]
    {
        get => _values[index];
        set => _values[index] = value;
    }
    protected CostCategory(string? baseUrn)
    {
        BaseUrn = baseUrn;
        CalculateRating();
    }

    protected string? BaseUrn { get; }
    public abstract string Name { get; }
    public abstract string Label { get; }
    public TagColour Colour { get; protected set; }
    public string? DisplayText { get; protected set; }
    public string? Priority { get; protected set; }
    public int Decile { get; protected set; }
    public decimal Value => Values.SingleOrDefault(x => BaseUrn == x.Key).Value?.Actual ?? throw new ArgumentNullException(nameof(Value));
    protected decimal[] ValuesAsArray => Values.Select(x => x.Value.Actual).ToArray();
    public int Percentage => (int)Math.Round((decimal)Values.Count(x => x.Value.Actual < Value) / ValuesAsArray.Length * 100, 0, MidpointRounding.AwayFromZero);
    public ReadOnlyDictionary<string, Category> Values => _values.AsReadOnly();
    protected virtual void CalculateRating()
    {
        Colour = TagColour.Yellow;
        DisplayText = "Medium priority";
        Priority = "priority medium";
        Decile = 3;
    }

    public abstract void Add(string urn, SchoolExpenditure expenditure);
}


public class AdministrativeSupplies(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Administrative supplies";
    public override string Label => "per pupil";

    protected override void CalculateRating()
    {
        Colour = TagColour.Red;
        DisplayText = "High priority";
        Priority = "priority high";
        Decile = 3;
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.AdministrativeSuppliesCosts, expenditure);
    }
}

public class CateringStaffServices(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Catering staff and services";
    public override string Label => "per pupil";

    protected override void CalculateRating()
    {
        Colour = TagColour.Grey;
        DisplayText = "Low priority";
        Priority = "priority low";
        Decile = 3;
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.NetCateringCosts, expenditure);
    }
}

public class EducationalIct(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Educational ICT";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.LearningResourcesIctCosts, expenditure);
    }
}

public class EducationalSupplies(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Educational supplies";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalEducationalSuppliesCosts, expenditure);
    }
}

public class NonEducationalSupportStaff(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Non-educational support staff";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts, expenditure);
    }
}

public class TeachingStaff(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Teaching and teaching supply staff";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalTeachingSupportStaffCosts, expenditure);
    }
}

public class Other(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Other";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalOtherCosts, expenditure);
    }
}

public class PremisesStaffServices(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Premises staff and services";
    public override string Label => "per square metre";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalPremisesStaffServiceCosts, expenditure);
    }
}

public class Utilities(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Utilities";
    public override string Label => "per square metre";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalUtilitiesCosts, expenditure);
    }
}

public static class CategoryBuilder
{
    public static IEnumerable<CostCategory> Build(
        IEnumerable<SchoolExpenditure> pupilExpenditure, IEnumerable<SchoolExpenditure> areaExpenditure, string? baseUrn)
    {
        var teachingStaff = new TeachingStaff(baseUrn);
        var administrativeSupplies = new AdministrativeSupplies(baseUrn);
        var cateringStaffServices = new CateringStaffServices(baseUrn);
        var educationalIct = new EducationalIct(baseUrn);
        var educationalSupplies = new EducationalSupplies(baseUrn);
        var nonEducationalSupportStaff = new NonEducationalSupportStaff(baseUrn);
        var other = new Other(baseUrn);
        var premisesStaffServices = new PremisesStaffServices(baseUrn);
        var utilities = new Utilities(baseUrn);

        foreach (var expenditure in pupilExpenditure)
        {
            var urn = expenditure.Urn;
            ArgumentNullException.ThrowIfNull(urn);

            teachingStaff.Add(urn, expenditure);
            administrativeSupplies.Add(urn, expenditure);
            cateringStaffServices.Add(urn, expenditure);
            educationalIct.Add(urn, expenditure);
            educationalSupplies.Add(urn, expenditure);
            nonEducationalSupportStaff.Add(urn, expenditure);
            other.Add(urn, expenditure);
        }

        foreach (var expenditure in areaExpenditure)
        {
            var urn = expenditure.Urn;
            ArgumentNullException.ThrowIfNull(urn);

            premisesStaffServices.Add(urn, expenditure);
            utilities.Add(urn, expenditure);
        }

        return new CostCategory[]
        {
            teachingStaff,
            administrativeSupplies,
            cateringStaffServices,
            educationalIct,
            educationalSupplies,
            nonEducationalSupportStaff,
            other,
            premisesStaffServices,
            utilities
        };
    }

    public static IEnumerable<CostCategory> Build(IEnumerable<SchoolExpenditure> expenditure, string baseUrn)
    {
        var teachingStaff = new TeachingStaff(baseUrn);
        var administrativeSupplies = new AdministrativeSupplies(baseUrn);
        var cateringStaffServices = new CateringStaffServices(baseUrn);
        var educationalIct = new EducationalIct(baseUrn);
        var educationalSupplies = new EducationalSupplies(baseUrn);
        var nonEducationalSupportStaff = new NonEducationalSupportStaff(baseUrn);
        var other = new Other(baseUrn);
        var premisesStaffServices = new PremisesStaffServices(baseUrn);
        var utilities = new Utilities(baseUrn);

        foreach (var value in expenditure)
        {
            var urn = value.Urn;
            ArgumentNullException.ThrowIfNull(urn);

            teachingStaff.Add(urn, value);
            administrativeSupplies.Add(urn, value);
            cateringStaffServices.Add(urn, value);
            educationalIct.Add(urn, value);
            educationalSupplies.Add(urn, value);
            nonEducationalSupportStaff.Add(urn, value);
            other.Add(urn, value);
            premisesStaffServices.Add(urn, value);
            utilities.Add(urn, value);
        }

        return new CostCategory[]
        {
            teachingStaff,
            administrativeSupplies,
            cateringStaffServices,
            educationalIct,
            educationalSupplies,
            nonEducationalSupportStaff,
            other,
            premisesStaffServices,
            utilities
        };
    }
}