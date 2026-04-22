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
        SubCategoryFilter.Total => "Total pupils with EHC plans",
        SubCategoryFilter.Mainstream => "Placement of pupils with EHC plans in mainstream schools or academies",
        SubCategoryFilter.Resourced => "Placement of pupils with EHC plans resourced provision or SEN units",
        SubCategoryFilter.Special => "Placement of pupils with EHC plans maintained special school or special academies",
        SubCategoryFilter.Independent => "Placement of pupils with EHC plans NMSS or independent schools",
        SubCategoryFilter.Hospital => "Placement of pupils with EHC plans in hospital schools or alternative provisions",
        SubCategoryFilter.Post16 => "Placement of pupils with EHC plans in post 16",
        SubCategoryFilter.Other => "Placement of pupils with EHC plans in other types of provisions",
        _ => ""
    };

    public static string GetTableHeading(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.Total => "Total pupils with EHC plans (per 1000 pupils)",
        SubCategoryFilter.Mainstream => "EHC plans in Mainstream schools or academies (per 1000 pupils)",
        SubCategoryFilter.Resourced => "EHC plans in Resourced provision or SEN units (per 1000 pupils)",
        SubCategoryFilter.Special => "EHC plans in Maintained special school or special academies (per 1000 pupils)",
        SubCategoryFilter.Independent => "EHC plans in NMSS or independent schools (per 1000 pupils)",
        SubCategoryFilter.Hospital => "EHC plans in Hospital schools or alternative provisions (per 1000 pupils)",
        SubCategoryFilter.Post16 => "EHC plans in Post 16 (per 1000 pupils)",
        SubCategoryFilter.Other => "EHC plans in other types of provisions (per 1000 pupils)",
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