namespace Web.App.ViewModels.Components;

public class AppInsightsViewModel(
    string connectionString,
    bool analyticsCookiesEnabled)
{
    public string ConnectionString => connectionString;
    public string? OperationId { get; set; }
    public bool AnalyticsCookiesEnabled => analyticsCookiesEnabled;
}
