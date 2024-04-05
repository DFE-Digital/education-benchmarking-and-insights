using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<RagRating> ratings,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure)
{
    private readonly CostCategory[] _categories = CategoryBuilder.Build(ratings, pupilExpenditure, areaExpenditure).ToArray();

    public string? Name => school.Name;
    public string? Urn => school.Urn;
    public bool IsPartOfTrust => school.IsPartOfTrust;

    public IEnumerable<CostCategory> PriorityCosts => _categories
        .Where(x => x.Rating.Status is "Red" or "Amber")
        .OrderBy(x => x.Rating.StatusOrder)
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<CostCategory> LowPriorityCosts => _categories
        .Where(x => x.Rating.Status is "Green")
        .OrderBy(x => x.Rating.StatusOrder)
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);
}