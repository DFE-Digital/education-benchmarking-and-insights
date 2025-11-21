namespace Web.App.ViewModels.Components;

public class DataSourceInsetViewModel(
    string[] dataSource,
    bool showKs4Progress,
    int ks4ProgressYear,
    string[]? additionText,
    string? compareSchoolPerformanceUrl)
{
    public string[] DataSource => dataSource;
    public bool ShowKs4Progress => showKs4Progress;
    public int Ks4ProgressYear => ks4ProgressYear;
    public string[] AdditionText => additionText ?? [];
    public string? CompareSchoolPerformanceUrl => compareSchoolPerformanceUrl;
}