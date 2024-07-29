namespace Web.App.ViewModels.Components;

public class AnalyticsViewModel(string instrumentationKey, bool analyticsCookiesEnabled)
{
    public string InstrumentationKey => instrumentationKey;
    public string? OperationId { get; set; }
    public bool AnalyticsCookiesEnabled => analyticsCookiesEnabled;
}