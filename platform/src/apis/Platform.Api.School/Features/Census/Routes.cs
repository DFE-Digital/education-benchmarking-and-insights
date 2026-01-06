namespace Platform.Api.School.Features.Census;

public static class Routes
{
    public const string History = $"schools/{Constants.UrnParam}/census/history";
    public const string Single = $"schools/{Constants.UrnParam}/census";
    public const string Collection = "schools/census";
    public const string UserDefined = $"schools/{Constants.UrnParam}/user-defined/{{identifier}}/census";
    public const string ComparatorSetAverageHistory = $"schools/{Constants.UrnParam}/comparator-set-average/census/history";
    public const string NationalAverageHistory = "schools/national-average/census/history";
    public const string SeniorLeadership = "schools/senior-leadership/census";
}