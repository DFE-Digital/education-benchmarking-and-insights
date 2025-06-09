using Web.App.Domain;
using Web.App.Services;

namespace Web.App.ViewModels;

public class SchoolSpendingViewModel(
    School school,
    IEnumerable<RagRating> ratings,
    IEnumerable<SchoolExpenditure> pupilExpenditure,
    IEnumerable<SchoolExpenditure> areaExpenditure,
    Dictionary<string, CommercialResourceLink[]> resources,
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

    public Dictionary<string, CommercialResourceLink[]> Resources => resources;

    public IEnumerable<CostCategory> HighPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "red");

    public IEnumerable<CostCategory> MediumPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "amber");

    public IEnumerable<CostCategory> LowPriorityCosts => Costs
        .Where(x => x.Rating.RAG is "green");


    public static ChartStatsViewModel Stats(RagRating rating)
    {
        return new ChartStatsViewModel
        {
            Average = rating.Median,
            Difference = rating.DiffMedian,
            PercentDifference = rating.Median switch
            {
                null => null,
                0 => 0,
                _ => rating.DiffMedian / rating.Median * 100
            }
        };
    }
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
    public bool IsPartOfTrust { get; set; }
    public CostCodes CostCodes => new(IsPartOfTrust);
}

public class RagRatingCommentaryViewModel(string prefix = "Spending is")
{
    public string Prefix => prefix;
    public RagRating? Rating { get; init; }
}