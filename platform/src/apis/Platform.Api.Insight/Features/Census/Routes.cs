namespace Platform.Api.Insight.Features.Census;

public static class Routes
{
    public const string SchoolHistory = "census/{urn}/history";
    public const string School = "census/{urn}";
    public const string SchoolCustom = "census/{urn}/custom/{identifier}";
    public const string SchoolHistoryComparatorSetAverage = "census/{urn}/history/comparator-set-average";
    public const string SchoolHistoryNationalAverage = "census/history/national-average";
    public const string SchoolsQuery = "census";
}