using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustSpendingViewModel(
    Trust trust,
    IReadOnlyCollection<School> schools,
    IEnumerable<RagRating> ratings,
    string[]? categories,
    string[]? priorities)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public int NumberSchools => schools.Count;

    public string[] CostCategories => (categories ?? [])
        .Where(c => !string.IsNullOrWhiteSpace(c))
        .ToArray();

    public bool IsPriorityHigh => priorities != null && priorities.Contains("high", StringComparer.OrdinalIgnoreCase);
    public bool IsPriorityMedium => priorities != null && priorities.Contains("medium", StringComparer.OrdinalIgnoreCase);
    public bool IsPriorityLow => priorities != null && priorities.Contains("low", StringComparer.OrdinalIgnoreCase);

    public IEnumerable<RagSchoolsSpendingByCategoryViewModel> RatingsByCategory => ratings
        .OrderBy(x => x.Category)
        .ThenBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
        .GroupBy(x => x.Category)
        .Select(x => new RagSchoolsSpendingByCategoryViewModel(
            x.Key,
            x.GroupBy(y => y.RAG)
                .Select(y => new RagSchoolsSpendingStatusViewModel(
                    y.Key,
                    y.Select(z => z.PriorityTag).FirstOrDefault(),
                    SelectSchools(y)
                ))
        ));

    public IEnumerable<RagSchoolsSpendingByStatusViewModel> RatingsByPriority => ratings
        .OrderBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
        .GroupBy(x => x.RAG)
        .Select(x => new RagSchoolsSpendingByStatusViewModel(
            x.Key,
            x.Select(z => z.PriorityTag).FirstOrDefault(),
            x.GroupBy(y => y.Category)
                .Select(y => new RagSchoolsSpendingCategoryViewModel(
                    y.Key,
                    SelectSchools(y)
                ))
                .OrderByDescending(o => o.Schools.Count())
        ));

    private IEnumerable<RagSchoolSpendingSchoolViewModel> SelectSchools(IGrouping<string?, RagRating> grouping)
    {
        return grouping.SelectMany(g => schools
                .Where(s => s.URN == g.URN)
                .Select(s => new RagSchoolSpendingSchoolViewModel(s, g)))
            .OrderByDescending(s => s.Value);
    }
}

public class RagSchoolsSpendingByCategoryViewModel(string? costCategory, IEnumerable<RagSchoolsSpendingStatusViewModel> statuses)
{
    public string? CostCategory => costCategory;
    public IEnumerable<RagSchoolsSpendingStatusViewModel> Statuses => statuses;
}

public class RagSchoolsSpendingStatusViewModel(
    string? status,
    (TagColour Colour, string DisplayText, string Class)? priorityTag,
    IEnumerable<RagSchoolSpendingSchoolViewModel> schools)
{
    public string? Status => status;
    public (TagColour Colour, string DisplayText, string Class)? PriorityTag => priorityTag;
    public IEnumerable<RagSchoolSpendingSchoolViewModel> Schools => schools;
}

public class RagSchoolsSpendingByStatusViewModel(
    string? status,
    (TagColour Colour, string DisplayText, string Class)? priorityTag,
    IEnumerable<RagSchoolsSpendingCategoryViewModel> categories)
{
    public string? Status => status;
    public (TagColour Colour, string DisplayText, string Class)? PriorityTag => priorityTag;
    public IEnumerable<RagSchoolsSpendingCategoryViewModel> Categories => categories;
}

public class RagSchoolsSpendingCategoryViewModel(string? category, IEnumerable<RagSchoolSpendingSchoolViewModel> schools)
{
    public string? Category => category;
    public IEnumerable<RagSchoolSpendingSchoolViewModel> Schools => schools;
}

public class RagSchoolSpendingSchoolViewModel(School? school, RagRating? rating)
{
    public string? Urn => school?.URN;
    public string? Name => school?.SchoolName;
    public decimal? Decile => rating?.Decile;
    public decimal? Value => rating?.Value;
}

public class TrustSpendingPriorityStatusViewModel(
    (TagColour Colour, string DisplayText, string Class)? priorityTag,
    int schoolsInStatus,
    int totalSchools)
{
    public (TagColour Colour, string DisplayText, string Class)? PriorityTag => priorityTag;
    public int SchoolsInStatus => schoolsInStatus;
    public int TotalSchools => totalSchools;
}