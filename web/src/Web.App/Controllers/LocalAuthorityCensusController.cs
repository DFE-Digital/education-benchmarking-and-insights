using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.LocalAuthorities)]
[Route("local-authority/{code}/census")]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.Census)]
public class LocalAuthorityCensusController(
    IEstablishmentApi establishmentApi,
    ILogger<LocalAuthorityCensusController> logger)
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
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityCensus(code);

                var localAuthority = await establishmentApi.GetLocalAuthority(code).GetResultOrThrow<LocalAuthority>();

                var query = new ApiQuery().AddIfNotNull("laCode", code);
                var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<School[]>();

                var phases = schools.GroupBy(x => x.OverallPhase).OrderByDescending(x => x.Count()).Select(x => x.Key).OfType<string>().ToArray();

                var viewModel = new LocalAuthorityCensusViewModel(localAuthority, phases);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority census: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}