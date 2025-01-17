namespace Platform.Domain;

public static class CostCategories
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

    public static readonly string[] All =
    [
        TeachingStaff,
        NonEducationalSupportStaff,
        EducationalSupplies,
        EducationalIct,
        PremisesStaffServices,
        Utilities,
        AdministrativeSupplies,
        CateringStaffServices,
        Other
    ];

    //TODO: Add unit test coverage
    public static bool IsValid(string? category) => All.Any(a => a.Equals(category, StringComparison.OrdinalIgnoreCase));
}