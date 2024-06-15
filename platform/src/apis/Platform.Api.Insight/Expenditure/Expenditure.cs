using System.Linq;

namespace Platform.Api.Insight.Expenditure;

public static class ExpenditureDimensions
{
    public const string Actuals = nameof(Actuals);
    public const string PerUnit = nameof(PerUnit);
    public const string PercentIncome = nameof(PercentIncome);
    public const string PercentExpenditure = nameof(PercentExpenditure);

    public static readonly string[] All =
    {
        Actuals,
        PerUnit,
        PercentIncome,
        PercentExpenditure
    };

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}

public static class ExpenditureCategories
{
    public const string TotalExpenditure = nameof(TotalExpenditure);
    public const string TeachingTeachingSupportStaff = nameof(TeachingTeachingSupportStaff);
    public const string NonEducationalSupportStaff = nameof(NonEducationalSupportStaff);
    public const string EducationalSupplies = nameof(EducationalSupplies);
    public const string EducationalIct = nameof(EducationalIct);
    public const string PremisesStaffServices = nameof(PremisesStaffServices);
    public const string Utilities = nameof(Utilities);
    public const string AdministrationSupplies = nameof(AdministrationSupplies);
    public const string CateringStaffServices = nameof(CateringStaffServices);
    public const string Other = nameof(Other);

    public static readonly string[] All =
    {
        TotalExpenditure,
        TeachingTeachingSupportStaff,
        NonEducationalSupportStaff,
        EducationalSupplies,
        EducationalIct,
        PremisesStaffServices,
        Utilities,
        AdministrationSupplies,
        CateringStaffServices,
        Other,
    };

    public static bool IsValid(string? category) => All.Any(a => a == category);
}