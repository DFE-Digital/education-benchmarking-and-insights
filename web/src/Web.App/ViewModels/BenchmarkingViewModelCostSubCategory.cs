namespace Web.App.ViewModels;

public class BenchmarkingViewModelCostSubCategory<T>
{
    public string? Uuid { get; init; }
    public string? SubCategory { get; init; }
    public string? TableHeading { get; init; }
    public string? ChartSvg { get; set; }
    public T[]? Data { get; init; }
}