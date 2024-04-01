using Newtonsoft.Json.Linq;
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

    public abstract string Name { get; }
    public abstract string Label { get; }
    public abstract void Add(string urn, SchoolExpenditure expenditure);

    public ReadOnlyDictionary<string, Category> Values => _values.AsReadOnly();
}


public class AdministrativeSupplies : CostCategory
{
    public override string Name => "Administrative supplies";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.AdministrativeSuppliesCosts, expenditure);
    }
}

public class CateringStaffServices : CostCategory
{
    public override string Name => "Catering staff and services";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.NetCateringCosts, expenditure);
    }
}

public class EducationalIct : CostCategory
{
    public override string Name => "Educational ICT";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.LearningResourcesIctCosts, expenditure);
    }
}

public class EducationalSupplies : CostCategory
{
    public override string Name => "Educational supplies";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalEducationalSuppliesCosts, expenditure);
    }
}

public class NonEducationalSupportStaff : CostCategory
{
    public override string Name => "Non-educational support staff";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts, expenditure);
    }
}

public class TeachingStaff : CostCategory
{
    public override string Name => "Teaching and teaching supply staff";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalTeachingSupportStaffCosts, expenditure);
    }
}

public class Other : CostCategory
{
    public override string Name => "Other";
    public override string Label => "per pupil";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalOtherCosts, expenditure);
    }
}

public class PremisesStaffServices : CostCategory
{
    public override string Name => "Premises staff and services";
    public override string Label => "per square metre";

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalPremisesStaffServiceCosts, expenditure);
    }
}

public class Utilities : CostCategory
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
    public static IEnumerable<(CostCategory, CostRating)> Build(
        IEnumerable<SchoolExpenditure> pupilExpenditure,
        IEnumerable<SchoolExpenditure> areaExpenditure,
        string? baseUrn,
        string? ofstedRating,
        bool usingCloseComparators)
    {
        var teachingStaff = new TeachingStaff();
        var administrativeSupplies = new AdministrativeSupplies();
        var cateringStaffServices = new CateringStaffServices();
        var educationalIct = new EducationalIct();
        var educationalSupplies = new EducationalSupplies();
        var nonEducationalSupportStaff = new NonEducationalSupportStaff();
        var other = new Other();
        var premisesStaffServices = new PremisesStaffServices();
        var utilities = new Utilities();

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

        var teachingStaffRating = new TeachingStaffRating(teachingStaff, baseUrn, usingCloseComparators, ofstedRating);
        var administrativeSuppliesRating = new AdministrativeSuppliesRating(administrativeSupplies, baseUrn);
        var cateringStaffServicesRating = new CateringStaffServicesRating(cateringStaffServices, baseUrn, usingCloseComparators);
        var educationalIctRating = new EducationalIctRating(educationalIct, baseUrn, usingCloseComparators, ofstedRating);
        var educationalSuppliesRating = new EducationalSuppliesRating(educationalSupplies, baseUrn, usingCloseComparators, ofstedRating);
        var nonEducationalSupportStaffRating = new NonEducationalSupportStaffRating(nonEducationalSupportStaff, baseUrn);
        var otherRating = new OtherRating(other, baseUrn);
        var premisesStaffServicesRating = new PremisesStaffServicesRating(premisesStaffServices, baseUrn);
        var utilitiesRating = new UtilitiesRating(utilities, baseUrn);

        return new List<(CostCategory CostCategory, CostRating CostRating)>
        {
            (CostCategory: teachingStaff, CostRating: teachingStaffRating),
            (CostCategory: administrativeSupplies, CostRating: administrativeSuppliesRating),
            (CostCategory: cateringStaffServices, CostRating: cateringStaffServicesRating),
            (CostCategory: educationalIct, CostRating: educationalIctRating),
            (CostCategory: educationalSupplies, CostRating: educationalSuppliesRating),
            (CostCategory: nonEducationalSupportStaff, CostRating: nonEducationalSupportStaffRating),
            (CostCategory: other, CostRating: otherRating),
            (CostCategory: premisesStaffServices, CostRating: premisesStaffServicesRating),
            (CostCategory: utilities, CostRating: utilitiesRating),
        };
    }

    public static IEnumerable<CostCategory> Build(IEnumerable<SchoolExpenditure> expenditure)
    {
        var teachingStaff = new TeachingStaff();
        var administrativeSupplies = new AdministrativeSupplies();
        var cateringStaffServices = new CateringStaffServices();
        var educationalIct = new EducationalIct();
        var educationalSupplies = new EducationalSupplies();
        var nonEducationalSupportStaff = new NonEducationalSupportStaff();
        var other = new Other();
        var premisesStaffServices = new PremisesStaffServices();
        var utilities = new Utilities();

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

public abstract class CostRating
{
    private IDecileFinder _decileFinder;

    public CostRating(CostCategory category, string? baseUrn, bool usingCloseComparators = false, string? ofstedRating = null)
    {
        IsGoodOrOutstanding = ofstedRating == "Outstanding" || ofstedRating == "Good";
        UsingCloseComparators = usingCloseComparators;
        Value = category.Values.SingleOrDefault(x => baseUrn == x.Key).Value.Value;
        ValuesAsArray = category.Values.Select(x => x.Value.Value).ToArray();
        Percentage = (int)Math.Round((decimal)ValuesAsArray.Count(x => x < Value) / ValuesAsArray.Length * 100, 0, MidpointRounding.AwayFromZero);
        _decileFinder = new FindFromLowest(Value, ValuesAsArray);
        Decile = _decileFinder.Find();
        SetRatingValues();
    }

    public bool IsGoodOrOutstanding { get; private set; }
    public bool UsingCloseComparators { get; private set; }
    public TagColour Colour { get; protected set; }
    public string? DisplayText { get; protected set; }
    public string? Priority { get; protected set; }
    public int Decile { get; private set; }
    public decimal Value { get; private set; }
    public decimal[]? ValuesAsArray { get; private set; }
    public int? Percentage { get; private set; }
    public abstract void SetRatingValues();
}

public class AdministrativeSuppliesRating(CostCategory category, string? baseUrn) : CostRating(category, baseUrn)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class CateringStaffServicesRating(CostCategory category, string? baseUrn, bool usingCloseComparators = false) : CostRating(category, baseUrn, usingCloseComparators)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = UsingCloseComparators
            ? [9, 10]
            : [10];

        int[] mediumRiskDeciles = UsingCloseComparators
            ? [4, 5, 6, 7, 8]
            : [4, 5, 6, 7, 8, 9];

        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class EducationalIctRating(CostCategory category, string? baseUrn, bool usingCloseComparators = false, string? ofstedRating = null) : CostRating(category, baseUrn, usingCloseComparators, ofstedRating)
{
    public override void SetRatingValues()
    {
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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class EducationalSuppliesRating(CostCategory category, string? baseUrn, bool usingCloseComparators = false, string? ofstedRating = null) : CostRating(category, baseUrn, usingCloseComparators, ofstedRating)
{
    public override void SetRatingValues()
    {
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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class NonEducationalSupportStaffRating(CostCategory category, string? baseUrn) : CostRating(category, baseUrn)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class TeachingStaffRating(CostCategory category, string? baseUrn, bool usingCloseComparators = false, string? ofstedRating = null) : CostRating(category, baseUrn, usingCloseComparators, ofstedRating)
{
    public override void SetRatingValues()
    {
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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class OtherRating(CostCategory category, string? baseUrn) : CostRating(category, baseUrn)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = [10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8, 9];
        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class PremisesStaffServicesRating(CostCategory category, string? baseUrn) : CostRating(category, baseUrn)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

public class UtilitiesRating(CostCategory category, string? baseUrn) : CostRating(category, baseUrn)
{
    public override void SetRatingValues()
    {
        int[] highRiskDeciles = [9, 10];
        int[] mediumRiskDeciles = [4, 5, 6, 7, 8];
        int[] lowRiskDeciles = [1, 2, 3];

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
                throw new ArgumentException($"Decile: {Decile} is not valid");
        }
    }
}

