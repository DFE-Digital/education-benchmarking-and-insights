namespace Web.App.ViewModels.Components;

public class AnalyticsViewModel(string instrumentationKey, bool trackingCookiesEnabled)
{
    public string InstrumentationKey => instrumentationKey;
    public string? OperationId { get; set; }
    public bool TrackingCookiesEnabled => trackingCookiesEnabled;
}