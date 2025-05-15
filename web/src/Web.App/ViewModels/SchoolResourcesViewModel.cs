using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolResourcesViewModel(School school, IEnumerable<RagRating> ratings, IEnumerable<CommercialResources> resources)
{
    private readonly CostCategory[] _categories = CategoryBuilder.Build(ratings, Array.Empty<SchoolExpenditure>(), Array.Empty<SchoolExpenditure>()).ToArray();

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool HasMetricRag => ratings.Any();

    public IEnumerable<CostCategory> CostCategories => _categories
        .Where(x => x.Rating.RAG is "red" or "amber" && x.Rating.Category is not Category.Other)
        .Where(x => x.CanShowCommercialResources)
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<GroupedResources> GroupedResources => CommercialResourcesBuilder.GroupByValidCategory(resources);
    public (string? Title, string? Url)? AllResourcesFramework => CommercialResourcesBuilder.GetFindAFrameworkLink(resources);
}