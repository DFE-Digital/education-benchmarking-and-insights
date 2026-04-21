namespace Platform.Domain;

//TODO: Consider converting these to enums
public static class CostCategories
{
    /// <summary>Costs related to teaching staff and direct educational support.</summary>
    public const string TeachingStaff = "Teaching and Teaching support staff";
    /// <summary>Costs for staff providing non-educational support (e.g., administration, welfare).</summary>
    public const string NonEducationalSupportStaff = "Non-educational support staff and services";
    /// <summary>Costs of resources directly used for teaching and learning.</summary>
    public const string EducationalSupplies = "Educational supplies";
    /// <summary>Costs associated with educational Information and Communication Technology hardware and software.</summary>
    public const string EducationalIct = "Educational ICT";
    /// <summary>Costs related to building maintenance, premises staff, and related services.</summary>
    public const string PremisesStaffServices = "Premises staff and services";
    /// <summary>Costs for energy and water utilities.</summary>
    public const string Utilities = "Utilities";
    /// <summary>Costs for office supplies, printing, and general administration.</summary>
    public const string AdministrativeSupplies = "Administrative supplies";
    /// <summary>Costs related to providing school meals and catering staff.</summary>
    public const string CateringStaffServices = "Catering staff and supplies";
    /// <summary>Miscellaneous costs not covered by other specific categories.</summary>
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