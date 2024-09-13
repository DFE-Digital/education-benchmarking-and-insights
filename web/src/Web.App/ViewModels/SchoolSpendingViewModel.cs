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

    public IEnumerable<CostCategory> HighPriorityCosts => _categories
        .Where(x => x.Rating.RAG is "red")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<CostCategory> MediumPriorityCosts => _categories
        .Where(x => x.Rating.RAG is "amber")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public IEnumerable<CostCategory> LowPriorityCosts => _categories
        .Where(x => x.Rating.RAG is "green")
        .OrderBy(x => Lookups.StatusOrderMap[x.Rating.RAG ?? string.Empty])
        .ThenByDescending(x => x.Rating.Decile)
        .ThenByDescending(x => x.Rating.Value);

    public bool HasIncompleteData => pupilExpenditure.Concat(areaExpenditure).Any(x => x.HasIncompleteData);

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
    
    public Dictionary<string, string> Lookup1 => new()
    {
        { "Administrative supplies", "_AdministrativeSupplies" },
        { "Catering staff and supplies", "_CateringStaffServices" },
        { "Educational ICT", "_EducationalICT" },
        { "Educational supplies", "_EducationalSupplies" },
        { "Non-educational support staff and services", "_NonEducationalSupportStaff" },
        { "Other costs", "_OtherCosts" },
        { "Premises staff and services", "_PremisesStaffServices" },
        { "Teaching and Teaching support staff", "_TeachingStaff" },
        { "Utilities", "_Utilities" }
    };
    
    public Dictionary<string, bool> Lookup2 => new()
    {
        { "Administrative supplies", false },
        { "Catering staff and supplies", false },
        { "Educational ICT", false },
        { "Educational supplies", false },
        { "Non-educational support staff", false },
        { "Other costs", true },
        { "Premises staff and services", true },
        { "Teaching and Teaching support staff", true },
        { "Utilities", true }
    };
}