using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/risks")]
[ValidateLaCode]
[FeatureGate(FeatureFlags.LocalAuthorityRiskIndicators)]
public class LocalAuthorityRisksController(
    ILogger<LocalAuthorityRisksController> logger,
    ILocalAuthorityApi api,
    IFinanceService financeService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string code,
        string? sortField,
        string? sortOrder,
        int? page,
        string? selectedPhaseOption)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {
                sortField ??= nameof(LocalAuthorityRiskIndicators.Overall);
                sortOrder ??= "desc";
                page ??= 1;
                selectedPhaseOption ??= OverallPhaseTypes.AllPhasesLabel;

                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();
                var years = await financeService.GetYears();

                var risksQuery = BuildQuery(code, sortField, sortOrder, page, selectedPhaseOption);
                var result = await api.QueryRisksAsync(risksQuery).GetPagedResultOrThrow<LocalAuthorityRiskIndicators>();

                var viewModel = new LocalAuthorityRisksViewModel(la, years.Cfr, result)
                {
                    SortField = sortField,
                    SortOrder = sortOrder,
                    SelectedPhaseOption = selectedPhaseOption
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority risk indicators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(
        string code,
        string? currentSort,
        string? selectedPhaseOption)
    {
        var parts = currentSort?.Split('~');
        var sortField = parts?[0];
        var sortOrder = parts?[1];

        return RedirectToAction("Index", new
        {
            code,
            sortField,
            sortOrder,
            selectedPhaseOption
        });
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download(
        string code,
        string? selectedPhaseOption)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                selectedPhaseOption ??= OverallPhaseTypes.AllPhasesLabel;

                var risks = await GetAllRisks(code, selectedPhaseOption);

                var csvRows = risks.Select(r => new LocalAuthorityRiskIndicatorsCsv
                {
                    SchoolName = r.SchoolName,
                    Urn = r.Urn,
                    OverallRiskScore = r.Overall,
                    FinancialRiskScore = r.Financial,
                    PupilAndWorkforceRiskScore = r.SchoolAndPupil,
                    EducationalPerformanceRiskScore = r.EducationalPerformance
                });

                return new CsvResults([new CsvResult(csvRows, $"{code}-schools-risk-overview.csv")], $"{code}-schools-risk-overview.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading local authority risk indicators: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthorityRiskIndicators[]> GetAllRisks(string code, string selectedPhaseOption)
    {
        var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

        var page = 1;
        var shouldGetNextPage = true;
        List<LocalAuthorityRiskIndicators> allRisks = [];

        while (shouldGetNextPage)
        {
            var risksQuery = BuildQuery(la.Code!, null, null, page, selectedPhaseOption, pageSize: 100);
            var result = await api.QueryRisksAsync(risksQuery).GetPagedResultOrThrow<LocalAuthorityRiskIndicators>();
            var risks = result.Results?.ToList() ?? [];

            allRisks.AddRange(risks);
            shouldGetNextPage = result.HasNextPage;
            page++;
        }

        return allRisks.ToArray();
    }

    private static ApiQuery BuildQuery(
        string code,
        string? sortField,
        string? sortOrder,
        int? page,
        string? selectedPhase = OverallPhaseTypes.AllPhasesLabel,
        int? pageSize = null)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("code", code);
        query.AddIfNotNull("sortField", sortField);
        query.AddIfNotNull("sortOrder", sortOrder);
        query.AddIfNotNull("page", page.ToString());
        query.AddIfNotNull("pageSize", pageSize?.ToString());
        if (selectedPhase != OverallPhaseTypes.AllPhasesLabel)
        {
            query.AddIfNotNull("phase", selectedPhase);
        }
        return query;
    }
}

public class LocalAuthorityRiskIndicatorsCsv
{
    public string SchoolName { get; set; } = string.Empty;
    public string Urn { get; set; } = string.Empty;
    public decimal OverallRiskScore { get; set; }
    public decimal FinancialRiskScore { get; set; }
    public decimal PupilAndWorkforceRiskScore { get; set; }
    public decimal EducationalPerformanceRiskScore { get; set; }
}
