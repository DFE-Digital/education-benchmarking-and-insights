using Web.App.Domain;

namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<RagRating> ratings,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure,
    string? userDefinedSetId = null)
{
    private readonly CostCategory[] _categories = CategoryBuilder.Build(ratings, pupilExpenditure, areaExpenditure).ToArray();

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;

    public IEnumerable<CostCategory> PriorityCosts => _categories
        .Where(x => x.Rating.RAG is "red" or "amber")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<CostCategory> LowPriorityCosts => _categories
        .Where(x => x.Rating.RAG is "green")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public bool HasIncompleteData => pupilExpenditure.Concat(areaExpenditure).Any(x => x.HasIncompleteData);
}