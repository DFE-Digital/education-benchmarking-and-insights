using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/senior-leadership")]
[ValidateUrn]
[FeatureGate(FeatureFlags.SeniorLeadership)]
public class SchoolSeniorLeadershipController(
    ISchoolApi schoolApi,
    ILogger<SchoolSeniorLeadershipController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var school = await School(urn);

                var viewModel = new SchoolSeniorLeadershipViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school senior leadership: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<School> School(string urn) => await schoolApi
        .SingleAsync(urn)
        .GetResultOrThrow<School>();
}