namespace Web.App.ViewModels.Components;

public class AnalyticsViewModel(string connectionString, bool analyticsCookiesEnabled)
{
    public string ConnectionString => connectionString;
    public string? OperationId { get; set; }
    public bool AnalyticsCookiesEnabled => analyticsCookiesEnabled;
}