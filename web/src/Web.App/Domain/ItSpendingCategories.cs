namespace Web.App.Domain;

public static class ItSpendingCategories
{
    public enum SubCategoryFilter
    {
        AdministrationSoftwareSystems = 0,
        Connectivity = 1,
        ITLearningResources = 2,
        ITSupport = 3,
        LaptopsDesktopsTablets = 4,
        OnsiteServers = 5,
        OtherHardware = 6
    }

    public static readonly SubCategoryFilter[] All =
    [
        SubCategoryFilter.AdministrationSoftwareSystems,
        SubCategoryFilter.Connectivity,
        SubCategoryFilter.ITLearningResources,
        SubCategoryFilter.ITSupport,
        SubCategoryFilter.LaptopsDesktopsTablets,
        SubCategoryFilter.OnsiteServers,
        SubCategoryFilter.OtherHardware
    ];

    public static string GetFilterDescriptionForSchool(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.AdministrationSoftwareSystems => "Administration software and systems (E20D)",
        SubCategoryFilter.Connectivity => "Connectivity (E20A)",
        SubCategoryFilter.ITLearningResources => "IT learning resources (E20C)",
        SubCategoryFilter.ITSupport => "IT support (E20G)",
        SubCategoryFilter.LaptopsDesktopsTablets => "Laptops, desktops and tablets (E20E)",
        SubCategoryFilter.OnsiteServers => "Onsite servers (E20B)",
        SubCategoryFilter.OtherHardware => "Other hardware (E20F)",
        _ => throw new ArgumentException(nameof(filter))
    };

    public static string GetFilterDescriptionForTrust(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.AdministrationSoftwareSystems => "ICT costs: Administration software and systems",
        SubCategoryFilter.Connectivity => "ICT costs: Connectivity",
        SubCategoryFilter.ITLearningResources => "ICT costs: IT learning resources",
        SubCategoryFilter.ITSupport => "ICT costs: IT support",
        SubCategoryFilter.LaptopsDesktopsTablets => "ICT costs: Laptops, desktops and tablets",
        SubCategoryFilter.OnsiteServers => "ICT costs: Onsite servers",
        SubCategoryFilter.OtherHardware => "ICT costs: Other hardware",
        _ => throw new ArgumentException(nameof(filter))
    };

    public static string GetHeadingForTrust(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.AdministrationSoftwareSystems => "ICT costs: Administration software and systems",
        SubCategoryFilter.Connectivity => "ICT costs: Connectivity",
        SubCategoryFilter.ITLearningResources => "ICT costs: IT learning resources",
        SubCategoryFilter.ITSupport => "ICT costs: IT support",
        SubCategoryFilter.LaptopsDesktopsTablets => "ICT costs: Laptops, desktops and tablets",
        SubCategoryFilter.OnsiteServers => "ICT costs: Onsite servers",
        SubCategoryFilter.OtherHardware => "ICT costs: Other hardware",
        _ => throw new ArgumentException(nameof(filter))
    };

    public static string GetHeadingForSchool(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.AdministrationSoftwareSystems => "Administration software and systems E20D",
        SubCategoryFilter.Connectivity => "Connectivity E20A",
        SubCategoryFilter.ITLearningResources => "IT learning resources E20C",
        SubCategoryFilter.ITSupport => "IT support E20G",
        SubCategoryFilter.LaptopsDesktopsTablets => "Laptops, desktops and tablets E20E",
        SubCategoryFilter.OnsiteServers => "Onsite servers E20B",
        SubCategoryFilter.OtherHardware => "Other hardware E20F",
        _ => throw new ArgumentException(nameof(filter))
    };

    public static Func<ItSpend, decimal?> GetSelector(this SubCategoryFilter filter) => filter switch
    {
        SubCategoryFilter.AdministrationSoftwareSystems => s => s.AdministrationSoftwareAndSystems,
        SubCategoryFilter.Connectivity => s => s.Connectivity,
        SubCategoryFilter.ITLearningResources => s => s.ItLearningResources,
        SubCategoryFilter.ITSupport => s => s.ItSupport,
        SubCategoryFilter.LaptopsDesktopsTablets => s => s.LaptopsDesktopsAndTablets,
        SubCategoryFilter.OnsiteServers => s => s.OnsiteServers,
        SubCategoryFilter.OtherHardware => s => s.OtherHardware,
        _ => throw new ArgumentException(nameof(filter))
    };
}