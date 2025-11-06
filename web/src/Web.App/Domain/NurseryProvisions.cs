namespace Web.App.Domain;

public static class NurseryProvisions
{
    public enum NurseryProvisionFilter
    {
        HasNurseryClasses = 0,
        HasNoNurseryClasses = 1,
        NotApplicable = 2,
        NotRecorded = 3
    }

    public static readonly NurseryProvisionFilter[] AllFilters =
    [
        NurseryProvisionFilter.HasNurseryClasses,
        NurseryProvisionFilter.HasNoNurseryClasses,
        NurseryProvisionFilter.NotApplicable,
        NurseryProvisionFilter.NotRecorded
    ];

    public static string GetFilterDescription(this NurseryProvisionFilter filter) => filter switch
    {
        NurseryProvisionFilter.HasNurseryClasses => "Has nursery classes",
        NurseryProvisionFilter.HasNoNurseryClasses => "Has no nursery classes",
        NurseryProvisionFilter.NotApplicable => "Not applicable",
        NurseryProvisionFilter.NotRecorded => "Not recorded",
        _ => throw new ArgumentOutOfRangeException(nameof(filter))
    };

    public static string GetQueryParam(this NurseryProvisionFilter filter) => filter switch
    {
        NurseryProvisionFilter.HasNurseryClasses => "Has Nursery Classes",
        NurseryProvisionFilter.HasNoNurseryClasses => "No Nursery Classes",
        NurseryProvisionFilter.NotApplicable => "Not applicable",
        NurseryProvisionFilter.NotRecorded => "Not recorded",
        _ => throw new ArgumentOutOfRangeException(nameof(filter))
    };
}