namespace Web.App.ViewModels.Components;

public class DataSourceInsetViewModel(
    string[] dataSource,
    bool showKs4Progress,
    int ks4ProgressYear,
    bool showAttainment8,
    int attainment8Year,
    string[]? additionText,
    string? compareSchoolPerformanceUrl)
{
    public string[] DataSource => dataSource;
    public bool ShowKs4Progress => showKs4Progress;
    public int Ks4ProgressYear => ks4ProgressYear;
    public bool ShowAttainment8 => showAttainment8;
    public int Attainment8Year => attainment8Year;
    public string[] AdditionText => additionText ?? [];
    public string? CompareSchoolPerformanceUrl => compareSchoolPerformanceUrl;
}
