using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<RagRating> ratings,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure,
    string? userDefinedSetId = null,
    string? customDataId = null)
{
    private readonly CostCategory[] _categories = CategoryBuilder.Build(ratings, pupilExpenditure, areaExpenditure).ToArray();

    public string? Name => school.SchoolName;
    public string? Urn => school.URN;
    public bool IsPartOfTrust => school.IsPartOfTrust;
    public string? UserDefinedSetId => userDefinedSetId;

    public string? CustomDataId => customDataId;

    private IEnumerable<CostCategory> Costs => _categories
        .Where(x => x.Rating.Category is not Category.Other)
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<CostCategory> HighPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "red");

    public IEnumerable<CostCategory> MediumPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "amber");

    public IEnumerable<CostCategory> LowPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "green");

    public static ChartStatsViewModel Stats(RagRating rating) => new()
    {
        Average = rating.Mean,
        Difference = rating.DiffMean,
        PercentDifference = rating.Mean switch
        {
            null => null,
            0 => 0,
            _ => rating.DiffMean / rating.Mean * 100
        }
    };
}

public class ChartStatsViewModel
{
    public decimal? Average { get; set; }
    public decimal? Difference { get; set; }
    public decimal? PercentDifference { get; set; }
}

public class CostsViewModel
{
    public IEnumerable<CostCategory> Costs { get; init; } = Array.Empty<CostCategory>();
    public string? Id { get; init; }
    public string? Urn { get; init; }
    public bool HasIncompleteData { get; init; }
    public bool IsCustomData { get; set; }
}

public class RagRatingCommentaryViewModel
{
    public RagRating Rating { get; init; }
}