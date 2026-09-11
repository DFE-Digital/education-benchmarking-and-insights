namespace Web.App.ViewModels.Components;

public class AppInsightsViewModel(
    string connectionString,
    string? cookiePolicy)
{
    public string ConnectionString => connectionString;
    public string? OperationId { get; set; }
    public bool AnalyticsCookiesEnabled => cookiePolicy == "enabled";

    public string ConsentStatus => cookiePolicy switch
    {
        "enabled" => "opt_in",
        "disabled" => "opt_out_explicit",
        _ => "opt_out_implicit"
    };
}
