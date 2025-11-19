namespace Web.App.ViewModels.Components;

public class DataSourceInsetViewModel(string[] dataSource, bool showKs4Progress, string? urn, int ks4ProgressYear, string[]? additionText)
{
    public string[] DataSource => dataSource;
    public bool ShowKs4Progress => showKs4Progress;
    public string? Urn => urn;
    public int Ks4ProgressYear => ks4ProgressYear;
    public string[] AdditionText => additionText ?? [];
}