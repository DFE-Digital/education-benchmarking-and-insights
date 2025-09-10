// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Infrastructure.Apis;

public record SchoolSuggestRequest
{
    public string? SearchText { get; set; }
    public int? Size { get; set; }
    public string[]? Exclude { get; set; }
    public bool? ExcludeMissingFinancialData { get; set; }
}