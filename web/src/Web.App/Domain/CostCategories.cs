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


public abstract class CostCategory(RagRating rating)
{
    private readonly Dictionary<string, Category> _values = new();

    protected Category this[string index]
    {
        get => _values[index];
        set => _values[index] = value;
    }
    public RagRating Rating => rating;
    public abstract void Add(string urn, SchoolExpenditure expenditure);

    public ReadOnlyDictionary<string, Category> Values => _values.AsReadOnly();
}


public class AdministrativeSupplies(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.AdministrativeSuppliesCosts, expenditure);
    }
}

public class CateringStaffServices(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.NetCateringCosts, expenditure);
    }
}

public class EducationalIct(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.LearningResourcesIctCosts, expenditure);
    }
}

public class EducationalSupplies(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalEducationalSuppliesCosts, expenditure);
    }
}

public class NonEducationalSupportStaff(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts, expenditure);
    }
}

public class TeachingStaff(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalTeachingSupportStaffCosts, expenditure);
    }
}

public class Other(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new PupilCategory(expenditure.TotalOtherCosts, expenditure);
    }
}

public class PremisesStaffServices(RagRating rating) : CostCategory(rating)
{
    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalPremisesStaffServiceCosts, expenditure);
    }
}

public class Utilities(RagRating rating) : CostCategory(rating)
{

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new AreaCategory(expenditure.TotalUtilitiesCosts, expenditure);
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
            if (rating.CostGroup == "Area")
            {
                switch (rating.CostCategoryId)
                {
                    case 5:
                        area.Add(new PremisesStaffServices(rating));
                        break;
                    case 6:
                        area.Add(new Utilities(rating));
                        break;
                }
            }
            else
            {
                switch (rating.CostCategoryId)
                {
                    case 1:
                        pupil.Add(new TeachingStaff(rating));
                        break;
                    case 2:
                        pupil.Add(new NonEducationalSupportStaff(rating));
                        break;
                    case 3:
                        pupil.Add(new EducationalSupplies(rating));
                        break;
                    case 4:
                        pupil.Add(new EducationalIct(rating));
                        break;
                    case 7:
                        pupil.Add(new AdministrativeSupplies(rating));
                        break;
                    case 8:
                        pupil.Add(new CateringStaffServices(rating));
                        break;
                    case 9:
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
            var urn = expenditure.Urn;
            ArgumentNullException.ThrowIfNull(urn);

            foreach (var category in categories.Pupil)
            {
                category.Add(urn, expenditure);
            }
        }

        foreach (var expenditure in areaExpenditure)
        {
            var urn = expenditure.Urn;
            ArgumentNullException.ThrowIfNull(urn);

            foreach (var category in categories.Area)
            {
                category.Add(urn, expenditure);
            }
        }

        return categories.Pupil.Concat(categories.Area);
    }
}