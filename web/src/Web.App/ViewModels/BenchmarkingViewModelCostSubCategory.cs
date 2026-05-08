using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels;

public class BenchmarkingViewModelCostSubCategory<T>
{
    public string? Uuid { get; init; }
    public string? SubCategory { get; init; }
    public int? SubCategoryId { get; init; }
    public string? ChartSvg { get; set; }
    public string[]? LineCodes { get; init; }
    public HighNeedsSpendingCategories.HighNeedsSpendingDataSourceInfoType AdditionalInfo { get; init; }
    public T[]? Data { get; init; }
}