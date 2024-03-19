namespace Web.App.Domain;

public abstract class Category(decimal actual, SchoolExpenditure expenditure)
{
    public abstract decimal Value { get; }
    public decimal Actual => actual;
    public decimal PercentageExpenditure => Actual / expenditure.TotalExpenditure * 100;
    public decimal PercentageIncome => Actual / expenditure.TotalExpenditure * 100;

    public const string AdministrativeSupplies = "Administrative supplies";
    public const string CateringStaffServices = "Catering staff and services";
    public const string EducationalIct = "Educational ICT";
    public const string EducationalSupplies = "Educational supplies";
    public const string NonEducationalSupportStaff = "Non-educational support staff";
    public const string Other = "Other";
    public const string PremisesStaffServices = "Premises staff and services";
    public const string TeachingStaff = "Teaching and teaching supply staff";
    public const string Utilities = "Utilities";

    public const string PerPupil = "per pupil";
    public const string PerSquareMetre = "per square metre";

    public static string LookUpLabel(string description)
    {
        string label;
        switch (description)
        {
            case Utilities:
            case PremisesStaffServices:
                label = PerSquareMetre;
                break;
            default:
                label = PerPupil;
                break;
        }

        return label;
    }
}

public abstract class PupilCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => decimal.Round(_actual / _expenditure.NumberOfPupils, 2, MidpointRounding.AwayFromZero);
}

public abstract class AreaCategory(decimal actual, SchoolExpenditure expenditure) : Category(actual, expenditure)
{
    private readonly decimal _actual = actual;
    private readonly SchoolExpenditure _expenditure = expenditure;

    public override decimal Value => decimal.Round(_actual / _expenditure.FloorArea, 2, MidpointRounding.AwayFromZero);
}

public class AdministrativeSupplies(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.AdministrativeSuppliesCosts, expenditure);

public class CateringStaffServices(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.CateringStaffCosts, expenditure);

public class EducationalIct(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.LearningResourcesIctCosts, expenditure);

public class EducationalSupplies(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.TotalEducationalSuppliesCosts, expenditure);

public class NonEducationalSupportStaff(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.TotalNonEducationalSupportStaffCosts, expenditure);

public class Other(SchoolExpenditure expenditure) : PupilCategory(expenditure.TotalOtherCosts, expenditure);

public class PremisesStaffServices(SchoolExpenditure expenditure)
    : AreaCategory(expenditure.TotalPremisesStaffServiceCosts, expenditure);

public class TeachingStaff(SchoolExpenditure expenditure)
    : PupilCategory(expenditure.TotalTeachingSupportStaffCosts, expenditure);

public class Utilities(SchoolExpenditure expenditure) : AreaCategory(expenditure.TotalUtilitiesCosts, expenditure);

public static class CategoryBuilder
{
    public static Dictionary<string, Dictionary<string, Category>> Build(
        IEnumerable<SchoolExpenditure> pupilExpenditure, IEnumerable<SchoolExpenditure> areaExpenditure)
    {
        var teachingStaff = new Dictionary<string, Category>();
        var administrativeSupplies = new Dictionary<string, Category>();
        var cateringStaffServices = new Dictionary<string, Category>();
        var educationalIct = new Dictionary<string, Category>();
        var educationalSupplies = new Dictionary<string, Category>();
        var nonEducationalSupportStaff = new Dictionary<string, Category>();
        var other = new Dictionary<string, Category>();
        var premisesStaffServices = new Dictionary<string, Category>();
        var utilities = new Dictionary<string, Category>();

        foreach (var expenditure in pupilExpenditure)
        {
            ArgumentNullException.ThrowIfNull(expenditure.Urn);

            teachingStaff[expenditure.Urn] = new TeachingStaff(expenditure);
            administrativeSupplies[expenditure.Urn] = new AdministrativeSupplies(expenditure);
            cateringStaffServices[expenditure.Urn] = new CateringStaffServices(expenditure);
            educationalIct[expenditure.Urn] = new EducationalIct(expenditure);
            educationalSupplies[expenditure.Urn] = new EducationalSupplies(expenditure);
            nonEducationalSupportStaff[expenditure.Urn] = new NonEducationalSupportStaff(expenditure);
            other[expenditure.Urn] = new Other(expenditure);
        }

        foreach (var expenditure in areaExpenditure)
        {
            ArgumentNullException.ThrowIfNull(expenditure.Urn);

            premisesStaffServices[expenditure.Urn] = new PremisesStaffServices(expenditure);
            utilities[expenditure.Urn] = new Utilities(expenditure);
        }

        return new Dictionary<string, Dictionary<string, Category>>
        {
            [Category.TeachingStaff] = teachingStaff,
            [Category.AdministrativeSupplies] = administrativeSupplies,
            [Category.CateringStaffServices] = cateringStaffServices,
            [Category.EducationalIct] = educationalIct,
            [Category.EducationalSupplies] = educationalSupplies,
            [Category.NonEducationalSupportStaff] = nonEducationalSupportStaff,
            [Category.Other] = other,
            [Category.PremisesStaffServices] = premisesStaffServices,
            [Category.Utilities] = utilities
        };
    }
}