using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/census")]
public class TrustCensusController(
    IEstablishmentApi establishmentApi,
    ILogger<TrustCensusController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new { companyNumber }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustCensus(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var viewModel = new TrustCensusViewModel(trust);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust census: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}