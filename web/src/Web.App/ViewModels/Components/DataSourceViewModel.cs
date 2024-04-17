namespace Web.App.ViewModels.Components;

public class DataSourceViewModel(string dataSource, string[]? additionText)
{
    public string DataSource => dataSource;
    public string[] AdditionText => additionText ?? Array.Empty<string>();
}