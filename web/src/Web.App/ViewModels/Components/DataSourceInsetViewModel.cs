using Web.App.Domain;

namespace Web.App.ViewModels.Components;

public class DataSourceInsetViewModel(string[] dataSource, Ks4ProgressProps props, int ks4ProgressYear, string[]? additionText)
{
    public string[] DataSource => dataSource;
    public bool ShowKs4Progress => props.Show;
    public string Urn => props.Urn;
    public int Ks4ProgressYear => ks4ProgressYear;
    public string[] AdditionText => additionText ?? [];
}