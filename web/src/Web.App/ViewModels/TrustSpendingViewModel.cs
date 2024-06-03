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

    public string[] CostCategories => categories ?? [];

    public bool IsPriorityHigh => priorities != null && priorities.Contains("high", StringComparer.OrdinalIgnoreCase);
    public bool IsPriorityMedium => priorities != null && priorities.Contains("medium", StringComparer.OrdinalIgnoreCase);
    public bool IsPriorityLow => priorities != null && priorities.Contains("low", StringComparer.OrdinalIgnoreCase);

    // todo: sorting; either here or in API
    public IEnumerable<RagSchoolsSpendingViewModel> Ratings => ratings
        .OrderBy(x => x.Category)
        .ThenBy(x => Lookups.StatusOrderMap[x.RAG ?? string.Empty])
        .GroupBy(x => x.Category)
        .Select(x => new RagSchoolsSpendingViewModel(
            x.Key,
            x.GroupBy(y => y.RAG)
                .Select(y => new RagSchoolsSpendingStatusViewModel(
                    y.Key,
                    y.Select(z => z.PriorityTag).FirstOrDefault(),
                    y.SelectMany(z => schools
                        .Where(s => s.URN == z.URN)
                        .Select(s => new RagSchoolSpendingSchoolViewModel(s, z)))
                        .OrderByDescending(s => s.Value)
                ))
        ));
}

public class RagSchoolsSpendingViewModel(string? costCategory, IEnumerable<RagSchoolsSpendingStatusViewModel> statuses)
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

public class RagSchoolSpendingSchoolViewModel(School? school, RagRating? rating)
{
    public string? Urn => school?.URN;
    public string? Name => school?.SchoolName;
    public decimal? Decile => rating?.Decile;
    public decimal? Value => rating?.Value;
}