namespace Web.App.Domain;

// todo: unit test
public static class SpecialProvisions
{
    public enum SpecialProvisionFilter
    {
        HasSpecialClasses = 0,
        HasNoSpecialClasses = 1,
        NotApplicable = 2,
        NotRecorded = 3
    }

    public static readonly SpecialProvisionFilter[] AllFilters =
    [
        SpecialProvisionFilter.HasSpecialClasses,
        SpecialProvisionFilter.HasNoSpecialClasses,
        SpecialProvisionFilter.NotApplicable,
        SpecialProvisionFilter.NotRecorded
    ];

    public static string GetFilterDescription(this SpecialProvisionFilter filter) => filter switch
    {
        SpecialProvisionFilter.HasSpecialClasses => "Has special classes",
        SpecialProvisionFilter.HasNoSpecialClasses => "Has no special classes",
        SpecialProvisionFilter.NotApplicable => "Not applicable",
        SpecialProvisionFilter.NotRecorded => "Not recorded",
        _ => throw new ArgumentException(nameof(filter))
    };
}