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


public abstract class CostCategory(string? baseUrn)
{
    private readonly Dictionary<string, Category> _values = new();

    protected Category this[string index]
    {
        get => _values[index];
        set => _values[index] = value;
    }

    protected string? BaseUrn = baseUrn;
    protected bool IsGoodOrOutstanding { get; set; }
    protected bool UsingCloseComparators { get; set; }
    public abstract string Name { get; }
    public abstract string Label { get; }
    public TagColour Colour { get; protected set; }
    public string? DisplayText { get; protected set; }
    public string? Priority { get; protected set; }
    public int Decile { get; protected set; }
    public decimal Value { get; protected set; }
    protected decimal[]? ValuesAsArray { get; private set; }
    public int? Percentage { get; protected set; }
    public ReadOnlyDictionary<string, Category> Values => _values.AsReadOnly();

    public abstract void CalculateRating();

    protected virtual void SetValues()
    {
        Value = _values.SingleOrDefault(x => BaseUrn == x.Key).Value.Value;
        ValuesAsArray = _values.Select(x => x.Value.Value).ToArray();
        Percentage = (int)Math.Round((decimal)ValuesAsArray.Count(x => x < Value) / ValuesAsArray.Length * 100, 0, MidpointRounding.AwayFromZero);
    }

    protected virtual void SetRatingValues(int[] highRiskDeciles, int[] mediumRiskDeciles, int[] lowRiskDeciles)
    {
        switch (Decile)
        {
            case var val when highRiskDeciles.Contains(val):
                Colour = TagColour.Red;
                DisplayText = "High priority";
                Priority = "priority high";
                break;
            case var val when mediumRiskDeciles.Contains(val):
                Colour = TagColour.Yellow;
                DisplayText = "Medium priority";
                Priority = "priority medium";
                break;
            case var val when lowRiskDeciles.Contains(val):
                Colour = TagColour.Grey;
                DisplayText = "Low priority";
                Priority = "priority low";
                break;
            default:
                throw new ArgumentException($"decile: {nameof(Decile)} is not valid");
        }
    }

    public abstract void Add(string urn, SchoolExpenditure expenditure);
}


public class AdministrativeSupplies(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Administrative supplies";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.AdministrativeSuppliesCosts, expenditure);
    }
}

public class CateringStaffServices : CostCategory
{
    public CateringStaffServices(string? baseUrn, bool usingCloseComparators) : base(baseUrn)
    {
        UsingCloseComparators = usingCloseComparators;
    }
    public override string Name => "Catering staff and services";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = UsingCloseComparators
            ? [9, 10]
            : [10];

        int[] mediumRiskDeciles = UsingCloseComparators
            ? [4, 5, 6, 7, 8]
            : [4, 5, 6, 7, 8, 9];

        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.NetCateringCosts, expenditure);
    }
}

public class EducationalIct : CostCategory
{
    public EducationalIct(string? baseUrn, string? ofstedRating, bool usingCloseComparators) : base(baseUrn)
    {
        if (ofstedRating == "Good" || ofstedRating == "Outstanding")
        {
            IsGoodOrOutstanding = true;
        }
        
        UsingCloseComparators = usingCloseComparators;
    }

    public override string Name => "Educational ICT";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [9, 10]
                : [10]
            : UsingCloseComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] mediumRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : UsingCloseComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] lowRiskDeciles = [4, 5, 6];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.LearningResourcesIctCosts, expenditure);
    }
}

public class EducationalSupplies : CostCategory
{
    public EducationalSupplies(string? baseUrn, string? ofstedRating, bool usingCloseComparators) : base(baseUrn)
    {
        if (ofstedRating == "Good" || ofstedRating == "Outstanding")
        {
            IsGoodOrOutstanding = true;
        }
        UsingCloseComparators = UsingCloseComparators;
    }

    public override string Name => "Educational supplies";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [9, 10]
                : [10]
            : UsingCloseComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] mediumRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : UsingCloseComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] lowRiskDeciles = [4, 5, 6];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalEducationalSuppliesCosts, expenditure);
    }
}

public class NonEducationalSupportStaff(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Non-educational support staff";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts, expenditure);
    }
}

public class TeachingStaff : CostCategory
{
    public TeachingStaff(string? baseUrn, string? ofstedRating, bool usingCloseComparators) : base(baseUrn)
    {
        if (ofstedRating == "Good" || ofstedRating == "Outstanding")
        {
            IsGoodOrOutstanding = true;
        }
        UsingCloseComparators = UsingCloseComparators;
    }

    public override string Name => "Teaching and teaching supply staff";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [9, 10]
                : [10]
            : UsingCloseComparators
                ? [1, 2, 9, 10]
                : [1, 10];

        int[] mediumRiskDeciles = IsGoodOrOutstanding
            ? UsingCloseComparators
                ? [1, 2, 3, 7, 8]
                : [1, 2, 3, 7, 8, 9]
            : UsingCloseComparators
                ? [3, 7, 8]
                : [2, 3, 7, 8, 9];

        int[] lowRiskDeciles = [4, 5, 6];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalTeachingSupportStaffCosts, expenditure);
    }
}

public class Other(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Other";
    public override string Label => "per pupil";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = [10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8, 9];
        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalOtherCosts, expenditure);
    }
}

public class PremisesStaffServices(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Premises staff and services";
    public override string Label => "per square metre";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalPremisesStaffServiceCosts, expenditure);
    }
}

public class Utilities(string? baseUrn) : CostCategory(baseUrn)
{
    public override string Name => "Utilities";
    public override string Label => "per square metre";

    public override void CalculateRating()
    {
        SetValues();

        ArgumentNullException.ThrowIfNull(ValuesAsArray);

        IDecileFinder decileCalculator = new FindFromLowest(Value, ValuesAsArray);
        Decile = decileCalculator.Find();

        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

        SetRatingValues(highRiskDeciles, mediumRiskDeciles, lowRiskDeciles);
    }

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalUtilitiesCosts, expenditure);
    }
}

public static class CategoryBuilder
{
    public static IEnumerable<CostCategory> Build(
        IEnumerable<SchoolExpenditure> pupilExpenditure, IEnumerable<SchoolExpenditure> areaExpenditure, string? baseUrn, string? ofstedRating, bool usingCloseComparators)
    {
        var teachingStaff = new TeachingStaff(baseUrn, ofstedRating, usingCloseComparators);
        var administrativeSupplies = new AdministrativeSupplies(baseUrn);
        var cateringStaffServices = new CateringStaffServices(baseUrn, usingCloseComparators);
        var educationalIct = new EducationalIct(baseUrn, ofstedRating, usingCloseComparators);
        var educationalSupplies = new EducationalSupplies(baseUrn, ofstedRating, usingCloseComparators);
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

        teachingStaff.CalculateRating();
        administrativeSupplies.CalculateRating();
        cateringStaffServices.CalculateRating();
        educationalIct.CalculateRating();
        educationalSupplies.CalculateRating();
        nonEducationalSupportStaff.CalculateRating();
        other.CalculateRating();
        premisesStaffServices.CalculateRating();
        utilities.CalculateRating();

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

    public static IEnumerable<CostCategory> Build(IEnumerable<SchoolExpenditure> expenditure, string baseUrn, string? ofstedRating, bool usingCloseComparators)
    {
        var teachingStaff = new TeachingStaff(baseUrn, ofstedRating, usingCloseComparators);
        var administrativeSupplies = new AdministrativeSupplies(baseUrn);
        var cateringStaffServices = new CateringStaffServices(baseUrn, usingCloseComparators);
        var educationalIct = new EducationalIct(baseUrn, ofstedRating, usingCloseComparators);
        var educationalSupplies = new EducationalSupplies(baseUrn, ofstedRating, usingCloseComparators);
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

        teachingStaff.CalculateRating();
        administrativeSupplies.CalculateRating();
        cateringStaffServices.CalculateRating();
        educationalIct.CalculateRating();
        educationalSupplies.CalculateRating();
        nonEducationalSupportStaff.CalculateRating();
        other.CalculateRating();
        premisesStaffServices.CalculateRating();
        utilities.CalculateRating();

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