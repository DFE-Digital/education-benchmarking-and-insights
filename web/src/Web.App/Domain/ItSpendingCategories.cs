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

    public static string GetFilterDescription(this SubCategoryFilter filter) => filter switch
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

    public static string GetHeading(this SubCategoryFilter filter) => filter switch
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

    public static Func<SchoolItSpend, decimal?> GetSelector(this SubCategoryFilter filter) => filter switch
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