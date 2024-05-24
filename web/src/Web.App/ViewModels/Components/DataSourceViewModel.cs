namespace Web.App.ViewModels.Components;

public class DataSourceViewModel(string dataSource, string[]? additionText, string wrapperClassName, string className)
{
    public string DataSource => dataSource;
    public string[] AdditionText => additionText ?? [];
    public string WrapperClassName => wrapperClassName;
    public string ClassName => className;
}