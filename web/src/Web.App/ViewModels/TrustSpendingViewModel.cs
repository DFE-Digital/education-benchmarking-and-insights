using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustSpendingViewModel(Trust trust, IReadOnlyCollection<School> schools, IEnumerable<RagRating> ratings)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.Name;
    public int NumberSchools => schools.Count;

    // todo: sorting; either here or in API
    public IEnumerable<RagSchoolsSpendingViewModel> Ratings => ratings
        .OrderBy(x => x.CostCategory)
        .ThenBy(x => x.StatusOrder)
        .GroupBy(x => x.CostCategory)
        .Select(x => new RagSchoolsSpendingViewModel(
            x.Key,
            x.GroupBy(y => y.Status)
                .Select(y => new RagSchoolsSpendingStatusViewModel(
                    y.Key,
                    y.Select(z => z.PriorityTag).FirstOrDefault(),
                    y.SelectMany(z => schools
                        .Where(s => s.Urn == z.Urn)
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
    public string? Urn => school?.Urn;
    public string? Name => school?.Name;
    public int? Decile => rating?.Decile;
    public decimal? Value => rating?.Value;
}