namespace Web.App.Domain;

public static class SixthFormProvisions
{
    public enum SixthFormProvisionFilter
    {
        HasSixthFormClasses = 0,
        HasNoSixthFormClasses = 1,
        NotApplicable = 2,
        NotRecorded = 3
    }

    public static readonly SixthFormProvisionFilter[] AllFilters =
    [
        SixthFormProvisionFilter.HasSixthFormClasses,
        SixthFormProvisionFilter.HasNoSixthFormClasses,
        SixthFormProvisionFilter.NotApplicable,
        SixthFormProvisionFilter.NotRecorded
    ];

    public static string GetFilterDescription(this SixthFormProvisionFilter filter) => filter switch
    {
        SixthFormProvisionFilter.HasSixthFormClasses => "Has a sixth form",
        SixthFormProvisionFilter.HasNoSixthFormClasses => "Does not have a sixth form",
        SixthFormProvisionFilter.NotApplicable => "Not applicable",
        SixthFormProvisionFilter.NotRecorded => "Not recorded",
        _ => throw new ArgumentOutOfRangeException(nameof(filter))
    };

    public static string GetQueryParam(this SixthFormProvisionFilter filter) => filter switch
    {
        SixthFormProvisionFilter.HasSixthFormClasses => "Has a sixth form",
        SixthFormProvisionFilter.HasNoSixthFormClasses => "Does not have a sixth form",
        SixthFormProvisionFilter.NotApplicable => "Not applicable",
        SixthFormProvisionFilter.NotRecorded => "Not recorded",
        _ => throw new ArgumentOutOfRangeException(nameof(filter))
    };
}