using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
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
        string? sort,
        string? selectedPhaseOption)
    {
        var parts = sort?.Split('~');
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


    private static ApiQuery BuildQuery(
        string code,
        string? sortField,
        string? sortOrder,
        int? page,
        string? selectedPhase = OverallPhaseTypes.AllPhasesLabel)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("code", code);
        query.AddIfNotNull("sortField", sortField);
        query.AddIfNotNull("sortOrder", sortOrder);
        query.AddIfNotNull("page", page.ToString());
        if (selectedPhase != OverallPhaseTypes.AllPhasesLabel)
        {
            query.AddIfNotNull("phase", selectedPhase);
        }
        return query;
    }
}
