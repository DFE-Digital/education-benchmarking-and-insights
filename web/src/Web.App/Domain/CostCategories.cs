using System.Collections.ObjectModel;
using Web.App.Extensions;
namespace Web.App.Domain;

public abstract class Category(decimal actual, SchoolExpenditure expenditure)
{
    public const string TeachingStaff = "Teaching and Teaching support staff";
    public const string NonEducationalSupportStaff = "Non-educational support staff and services";
    public const string EducationalSupplies = "Educational supplies";
    public const string EducationalIct = "Educational ICT";
    public const string PremisesStaffServices = "Premises staff and services";
    public const string Utilities = "Utilities";
    public const string AdministrativeSupplies = "Administrative supplies";
    public const string CateringStaffServices = "Catering staff and supplies";
    public const string Other = "Other costs";

    public abstract decimal Value { get; }
    public decimal Actual => actual;

    public decimal PercentageExpenditure => expenditure.TotalExpenditure == decimal.Zero
        ? 0
        : decimal.Round(Actual / (expenditure.TotalExpenditure ?? 0) * 100, 2, MidpointRounding.AwayFromZero);

    public decimal PercentageIncome => expenditure.TotalExpenditure == decimal.Zero
        ? 0
        : decimal.Round(Actual / (expenditure.TotalExpenditure ?? 0) * 100, 2, MidpointRounding.AwayFromZero);

    public static string? FromSlug(string? slug)
    {
        if (slug == TeachingStaff.ToSlug())
        {
            return TeachingStaff;
        }

        if (slug == NonEducationalSupportStaff.ToSlug())
        {
            return NonEducationalSupportStaff;
        }

        if (slug == EducationalSupplies.ToSlug())
        {
            return EducationalSupplies;
        }

        if (slug == EducationalIct.ToSlug())
        {
            return EducationalIct;
        }

        if (slug == PremisesStaffServices.ToSlug())
        {
            return PremisesStaffServices;
        }

        if (slug == Utilities.ToSlug())
        {
            return Utilities;
        }

        if (slug == AdministrativeSupplies.ToSlug())
        {
            return AdministrativeSupplies;
        }

        if (slug == CateringStaffServices.ToSlug())
        {
            return CateringStaffServices;
        }

        if (slug == Other.ToSlug())
        {
            return Other;
        }

        return null;
    }
}

public class PupilCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => _expenditure.NumberOfPupils == decimal.Zero
        ? 0
        : decimal.Round(_actual / (_expenditure.NumberOfPupils ?? 0), 0, MidpointRounding.AwayFromZero);
}

public class AreaCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => _expenditure.FloorArea == decimal.Zero
        ? 0
        : decimal.Round(_actual / _expenditure.FloorArea, 0, MidpointRounding.AwayFromZero);
}

public abstract class CostCategory(RagRating rating)
{
    private readonly Dictionary<string, Category> _values = new();

    protected Category this[string index]
    {
        get => _values[index];
        set => _values[index] = value;
    }
    public RagRating Rating => rating;

    public ReadOnlyDictionary<string, Category> Values => _values.AsReadOnly();
    public abstract void Add(string urn, SchoolExpenditure expenditure);
}

public class AdministrativeSupplies(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.AdministrativeSuppliesCosts ?? 0, expenditure);
    }
}

public class CateringStaffServices(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalGrossCateringCosts ?? 0, expenditure);
    }
}

public class EducationalIct(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.LearningResourcesIctCosts ?? 0, expenditure);
    }
}

public class EducationalSupplies(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalEducationalSuppliesCosts ?? 0, expenditure);
    }
}

public class NonEducationalSupportStaff(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts ?? 0, expenditure);
    }
}

public class TeachingStaff(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalTeachingSupportStaffCosts ?? 0, expenditure);
    }
}

public class Other(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalOtherCosts ?? 0, expenditure);
    }
}

public class PremisesStaffServices(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalPremisesStaffServiceCosts ?? 0, expenditure);
    }
}

public class Utilities(RagRating rating) : CostCategory(rating)
{

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalUtilitiesCosts ?? 0, expenditure);
    }
}

public static class CategoryBuilder
{
    private static (List<CostCategory> Pupil, List<CostCategory> Area) CategoriesFromRatings(IEnumerable<RagRating> ratings)
    {
        var pupil = new List<CostCategory>();
        var area = new List<CostCategory>();

        foreach (var rating in ratings)
        {
            if (Lookups.CategoryTypeMap[rating.Category ?? string.Empty] == "Building")
            {
                switch (rating.Category)
                {
                    case Category.PremisesStaffServices:
                        area.Add(new PremisesStaffServices(rating));
                        break;
                    case Category.Utilities:
                        area.Add(new Utilities(rating));
                        break;
                }
            }
            else
            {
                switch (rating.Category)
                {
                    case Category.TeachingStaff:
                        pupil.Add(new TeachingStaff(rating));
                        break;
                    case Category.NonEducationalSupportStaff:
                        pupil.Add(new NonEducationalSupportStaff(rating));
                        break;
                    case Category.EducationalSupplies:
                        pupil.Add(new EducationalSupplies(rating));
                        break;
                    case Category.EducationalIct:
                        pupil.Add(new EducationalIct(rating));
                        break;
                    case Category.AdministrativeSupplies:
                        pupil.Add(new AdministrativeSupplies(rating));
                        break;
                    case Category.CateringStaffServices:
                        pupil.Add(new CateringStaffServices(rating));
                        break;
                    case Category.Other:
                        pupil.Add(new Other(rating));
                        break;
                }
            }
        }

        return (pupil, area);
    }

    public static IEnumerable<CostCategory> Build(IEnumerable<RagRating> ratings,
        IEnumerable<SchoolExpenditure> pupilExpenditure, IEnumerable<SchoolExpenditure> areaExpenditure)
    {
        var categories = CategoriesFromRatings(ratings);

        foreach (var expenditure in pupilExpenditure)
        {
            var urn = expenditure.URN;
            ArgumentNullException.ThrowIfNull(urn);

            foreach (var category in categories.Pupil)
            {
                category.Add(urn, expenditure);
            }
        }

        foreach (var expenditure in areaExpenditure)
        {
            var urn = expenditure.URN;
            ArgumentNullException.ThrowIfNull(urn);

            foreach (var category in categories.Area)
            {
                category.Add(urn, expenditure);
            }
        }

        return categories.Pupil.Concat(categories.Area);
    }
}