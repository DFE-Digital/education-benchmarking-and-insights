using System;
using System.Linq;
namespace Platform.Api.Insight.Domain;

public static class CostCategory
{
    internal const string TeachingStaff = "Teaching and Teaching support staff";
    private const string NonEducationalSupportStaff = "Non-educational support staff and services";
    private const string EducationalSupplies = "Educational supplies";
    private const string EducationalIct = "Educational ICT";
    private const string PremisesStaffServices = "Premises staff and services";
    private const string Utilities = "Utilities";
    private const string AdministrativeSupplies = "Administrative supplies";
    private const string CateringStaffServices = "Catering staff and supplies";
    private const string Other = "Other costs";

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

    public static bool IsValid(string? category) => All.Any(a => a.Equals(category, StringComparison.OrdinalIgnoreCase));
}