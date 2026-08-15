using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.ViewModels;

public class LocalAuthorityRisksViewModel(LocalAuthority localAuthority, int cfrYear, PagedResults<LocalAuthorityRiskIndicators> result)
{
    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string?[] AvailablePhases => localAuthority.Schools.Select(x => x.OverallPhase).Distinct().ToArray();
    public int CfrYear => cfrYear;
    public IEnumerable<LocalAuthorityRiskIndicators> Results => result.Results ?? [];
    public string? SortField { get; init; }
    public string? SortOrder { get; init; }
    public string CurrentSort => $"{SortField}~{SortOrder}";
    public long TotalResults => result.TotalResults;
    public int Page => result.Page;
    public int PageSize => result.PageSize;
    public string? SelectedPhaseOption { get; init; }
    public string?[] ValidPhasesOptions => [OverallPhaseTypes.AllPhasesLabel, .. AvailablePhases];
    public string GlobalDefaultSort => $"{nameof(LocalAuthorityRiskIndicators.Overall)}~desc";
}
