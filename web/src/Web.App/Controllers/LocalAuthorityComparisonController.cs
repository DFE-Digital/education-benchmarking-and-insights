using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/comparison")]
[ValidateLaCode]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
public class LocalAuthorityComparisonController(
    IEstablishmentApi establishmentApi,
    ILogger<LocalAuthorityComparisonController> logger,
    ICostCodesService costCodesService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
               {
                   code
               }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityComparison(code);

                var localAuthority = await LocalAuthority(code);
                var costCodes = await costCodesService.GetCostCodes(false);
                var viewModel = new LocalAuthorityComparisonViewModel(localAuthority, costCodes);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();
}