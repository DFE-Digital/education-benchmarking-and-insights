using System.Collections.ObjectModel;
using Web.App.Extensions;
namespace Web.App.Domain;

public class Category(decimal value)
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

    public static readonly string[] InvertRagValueCategories = [TeachingStaff, EducationalSupplies, EducationalIct];

    public decimal Value => value;

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

    public abstract string[] SubCategories { get; }
    public abstract void Add(string urn, SchoolExpenditure expenditure);
}

public class AdministrativeSupplies(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.AdministrativeSupplies.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.AdministrativeSuppliesCosts ?? 0);
    }
}

public class CateringStaffServices(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.CateringStaffServices.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalGrossCateringCosts ?? 0);
    }
}

public class EducationalIct(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.EducationalIct.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.LearningResourcesIctCosts ?? 0);
    }
}

public class EducationalSupplies(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.EducationalSupplies.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalEducationalSuppliesCosts ?? 0);
    }
}

public class NonEducationalSupportStaff(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.NonEducationalSupportStaff.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalNonEducationalSupportStaffCosts ?? 0);
    }
}

public class TeachingStaff(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.TeachingStaff.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalTeachingSupportStaffCosts ?? 0);
    }
}

public class Other(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.Other.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalOtherCosts ?? 0);
    }
}

public class PremisesStaffServices(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.PremisesStaffServices.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalPremisesStaffServiceCosts ?? 0);
    }
}

public class Utilities(RagRating rating) : CostCategory(rating)
{
    public override string[] SubCategories => SubCostCategories.Utilities.SubCategories;

    public override void Add(string urn, SchoolExpenditure expenditure)
    {
        this[urn] = new Category(expenditure.TotalUtilitiesCosts ?? 0);
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

public static class SubCostCategories
{
    public static class TeachingStaff
    {
        public const string TeachingStaffCosts = "Teaching staff";
        public const string SupplyTeachingStaffCosts = "Supply teaching staff";
        public const string AgencySupplyTeachingStaffCosts = "Agency supply teaching staff";
        public const string EducationSupportStaffCosts = "Education support staff";
        public const string EducationalConsultancyCosts = "Educational consultancy";

        public static string[] SubCategories { get; } = [TeachingStaffCosts, SupplyTeachingStaffCosts, AgencySupplyTeachingStaffCosts, EducationSupportStaffCosts, EducationalConsultancyCosts];
    }

    public static class NonEducationalSupportStaff
    {
        public const string AdministrativeClericalStaffCosts = "Administrative and clerical staff";
        public const string AuditorsCosts = "Auditor costs";
        public const string OtherStaffCosts = "Other staff";
        public const string ProfessionalServicesNonCurriculumCosts = "Professional services (non-curriculum)";

        public static string[] SubCategories { get; } = [AdministrativeClericalStaffCosts, AuditorsCosts, OtherStaffCosts, ProfessionalServicesNonCurriculumCosts];
    }

    public static class EducationalSupplies
    {
        public const string ExaminationFeesCosts = "Examination fees";
        public const string LearningResourcesNonIctCosts = "Learning resources (not ICT equipment)";

        public static string[] SubCategories { get; } = [ExaminationFeesCosts, LearningResourcesNonIctCosts];
    }

    public static class EducationalIct
    {
        public const string LearningResourcesIctCosts = "ICT learning resources";

        public static string[] SubCategories { get; } = [LearningResourcesIctCosts];
    }

    public static class PremisesStaffServices
    {
        public const string CleaningCaretakingCosts = "Cleaning and caretaking";
        public const string MaintenancePremisesCosts = "Maintenance of premises";
        public const string OtherOccupationCosts = "Other occupation costs";
        public const string PremisesStaffCosts = "Premises staff";

        public static string[] SubCategories { get; } = [CleaningCaretakingCosts, MaintenancePremisesCosts, OtherOccupationCosts, PremisesStaffCosts];
    }

    public static class Utilities
    {
        public const string EnergyCosts = "Energy";
        public const string WaterSewerageCosts = "Water and sewerage";

        public static string[] SubCategories { get; } = [EnergyCosts, WaterSewerageCosts];
    }

    public static class AdministrativeSupplies
    {
        public const string AdministrativeSuppliesCosts = "Administrative supplies (non-educational)";

        public static string[] SubCategories { get; } = [AdministrativeSuppliesCosts];
    }

    public static class CateringStaffServices
    {
        public const string CateringStaffCosts = "Catering staff";
        public const string CateringSuppliesCosts = "Catering supplies";

        public static string[] SubCategories { get; } = [CateringStaffCosts, CateringSuppliesCosts];
    }

    public static class Other
    {
        public const string DirectRevenueFinancingCosts = "Direct revenue financing";
        public const string GroundsMaintenanceCosts = "Grounds maintenance";
        public const string IndirectEmployeeExpenses = "Indirect employee expenses";
        public const string InterestChargesLoanBank = "Interest charges for loan and bank";
        public const string OtherInsurancePremiumsCosts = "Other insurance premiums";
        public const string PrivateFinanceInitiativeCharges = "Private Finance Initiative (PFI) charges";
        public const string RentRatesCosts = "Rent and rates";
        public const string SpecialFacilitiesCosts = "Special facilities";
        public const string StaffDevelopmentTrainingCosts = "Staff development and training";
        public const string StaffRelatedInsuranceCosts = "Staff-related insurance";
        public const string SupplyTeacherInsurableCosts = "Supply teacher insurance";

        public static string[] SubCategories { get; } =
        [
            DirectRevenueFinancingCosts,
            GroundsMaintenanceCosts,
            IndirectEmployeeExpenses,
            InterestChargesLoanBank,
            OtherInsurancePremiumsCosts,
            PrivateFinanceInitiativeCharges,
            RentRatesCosts,
            SpecialFacilitiesCosts,
            StaffDevelopmentTrainingCosts,
            StaffRelatedInsuranceCosts,
            SupplyTeacherInsurableCosts
        ];
    }
}