namespace Web.App.ViewModels.Components;

public class AnalyticsViewModel(string instrumentationKey)
{
    public string InstrumentationKey => instrumentationKey;
    public PageViewTelemetry? Telemetry { get; set; }
}