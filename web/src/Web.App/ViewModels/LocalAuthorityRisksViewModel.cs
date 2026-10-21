using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.ViewModels;

public class LocalAuthorityRisksViewModel(
    LocalAuthority localAuthority,
    int cfrYear,
    PagedResults<LocalAuthorityRiskIndicators> result,
    DataSourceFileViewModel? riskScoresFile = null)
{
    private readonly LocalAuthorityRiskIndicators? _firstResult = result.Results?.FirstOrDefault();

    public string? Code => localAuthority.Code;
    public string? Name => localAuthority.Name;
    public string?[] AvailablePhases => localAuthority.Schools.Select(x => x.OverallPhase).Distinct().ToArray();
    public int CfrYear => cfrYear;
    public IEnumerable<LocalAuthorityRiskIndicators> Results => result.Results ?? [];
    public decimal? OverallMax => _firstResult?.OverallMax;
    public decimal? FinancialMax => _firstResult?.FinancialMax;
    public decimal? SchoolAndPupilMax => _firstResult?.SchoolAndPupilMax;
    public decimal? EducationalPerformanceMax => _firstResult?.EducationalPerformanceMax;
    public bool HasMaxGroupValues =>
        OverallMax.HasValue &&
        FinancialMax.HasValue &&
        SchoolAndPupilMax.HasValue &&
        EducationalPerformanceMax.HasValue;
    public string? SortField { get; init; }
    public string? SortOrder { get; init; }
    public string CurrentSort => $"{SortField}~{SortOrder}";
    public long TotalResults => result.TotalResults;
    public int Page => result.Page;
    public int PageSize => result.PageSize;
    public string? SelectedPhaseOption { get; init; }
    public string?[] ValidPhasesOptions => [OverallPhaseTypes.AllPhasesLabel, .. AvailablePhases];
    public string GlobalDefaultSort => $"{nameof(LocalAuthorityRiskIndicators.Overall)}~desc";
    public DataSourceFileViewModel? RiskScoresFile { get; } = riskScoresFile;
}
