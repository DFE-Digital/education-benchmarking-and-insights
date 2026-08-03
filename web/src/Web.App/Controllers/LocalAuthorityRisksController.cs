using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
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
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {
                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

                var years = await financeService.GetYears();

                var viewModel = new LocalAuthorityRisksViewModel(la, years.Cfr);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority risk indicators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}
