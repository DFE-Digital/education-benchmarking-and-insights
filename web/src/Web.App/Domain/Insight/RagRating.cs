using Web.App.Extensions;
namespace Web.App.Domain;

public record RagRating
{
    public string? URN { get; set; }
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public decimal? Value { get; set; }
    public decimal? Mean { get; set; }
    public decimal? DiffMean { get; set; }
    public decimal? PercentDiff { get; set; }
    public decimal? Percentile { get; set; }
    public decimal? Decile { get; set; }
    public string? RAG { get; set; }

    public string CostCategoryAnchorId => string.IsNullOrWhiteSpace(Category) ? string.Empty : Category.ToSlug();

    public (TagColour Colour, string DisplayText, string Class)? PriorityTag => Lookups.StatusPriorityMap[RAG ?? string.Empty];
    public string ResourcePartial => Lookups.CategoryResourcePartialMap[Category ?? string.Empty];
    public string Unit => Lookups.CategoryUnitMap[Category ?? string.Empty];
}

