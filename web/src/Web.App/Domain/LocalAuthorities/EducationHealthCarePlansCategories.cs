namespace Web.App.Domain.LocalAuthorities;

public static class EducationHealthCarePlansCategories
{
    public enum SubCategoryFilter
    {
        Total = 0,
        Mainstream = 1,
        Resourced = 2,
        Special = 3,
        Independent = 4,
        Hospital = 5,
        Post16 = 6,
        Other = 7
    }

    public static readonly SubCategoryFilter[] All =
    [
        SubCategoryFilter.Total,
        SubCategoryFilter.Mainstream,
        SubCategoryFilter.Resourced,
        SubCategoryFilter.Special,
        SubCategoryFilter.Independent,
        SubCategoryFilter.Hospital,
        SubCategoryFilter.Post16,
        SubCategoryFilter.Other
    ];

    public static string GetFilterDescription(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.Total => "Total EHC plans",
        SubCategoryFilter.Mainstream => "Mainstream schools or academies",
        SubCategoryFilter.Resourced => "Resourced provision or SEN units",
        SubCategoryFilter.Special => "Maintained special school or special academies",
        SubCategoryFilter.Independent => "NMSS or independent schools",
        SubCategoryFilter.Hospital => "Hospital schools or alternative provisions",
        SubCategoryFilter.Post16 => "Post 16",
        SubCategoryFilter.Other => "Other",
        _ => ""
    };

    public static string GetHeading(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.Total => "Total EHC plans",
        SubCategoryFilter.Mainstream => "EHC plans supported in mainstream schools or academies",
        SubCategoryFilter.Resourced => "EHC plans supported in resourced provision or SEN units",
        SubCategoryFilter.Special => "EHC plans supported in maintained special schools or special academies",
        SubCategoryFilter.Independent => "EHC plans supported in NMSS or independent schools",
        SubCategoryFilter.Hospital => "EHC plans supported in hospital schools or alternative provisions",
        SubCategoryFilter.Post16 => "EHC plans supported in post 16",
        SubCategoryFilter.Other => "EHC plans supported in other settings",
        _ => ""
    };

    public static decimal? GetValue(this SubCategoryFilter filter, EducationHealthCarePlans p) =>
        filter switch
        {
            SubCategoryFilter.Total => p.Total,
            SubCategoryFilter.Mainstream => p.Mainstream,
            SubCategoryFilter.Resourced => p.Resourced,
            SubCategoryFilter.Special => p.Special,
            SubCategoryFilter.Independent => p.Independent,
            SubCategoryFilter.Hospital => p.Hospital,
            SubCategoryFilter.Post16 => p.Post16,
            SubCategoryFilter.Other => p.Other,
            _ => null
        };


}