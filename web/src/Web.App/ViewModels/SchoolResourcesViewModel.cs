using Web.App.Domain;

namespace Web.App.ViewModels;

public record SchoolResourcesViewModelParams
{
    public required School School { get; init; }
    public required IEnumerable<RagRating> Ratings { get; init; }
    public required Dictionary<string, CommercialResourceLink[]> CategoryResources { get; init; }
    public required Dictionary<string, CommercialResourceLink[]> SubCategoryResources { get; init; }
}

public class SchoolResourcesViewModel(SchoolResourcesViewModelParams parameters)
{
    private readonly CostCategory[] _categories = CategoryBuilder.Build(parameters.Ratings).ToArray();

    public string? Name => parameters.School.SchoolName;
    public string? Urn => parameters.School.URN;
    public bool HasMetricRag => parameters.Ratings.Any();

    public IEnumerable<CostCategory> CostCategories => _categories
        .Where(x => x.Rating.RAG is "red" or "amber" && x.Rating.Category is not Category.Other)
        .Where(x => x.CanShowCommercialResources)
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public Dictionary<string, CommercialResourceLink[]> CategoryResources => parameters.CategoryResources;
    public Dictionary<string, CommercialResourceLink[]> Resources => parameters.SubCategoryResources;
}