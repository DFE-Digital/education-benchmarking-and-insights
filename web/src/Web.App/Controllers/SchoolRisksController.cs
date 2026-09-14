using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[LocalAuthorityAuthorization]
[Route("local-authority/{code}/risks/school/{urn}")]
[ValidateLaCode]
[FeatureGate(FeatureFlags.LocalAuthorityRiskIndicators)]
public class SchoolRisksController(
    ILogger<SchoolRisksController> logger,
    ISchoolApi schoolApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code, string urn)
    {
        using (logger.BeginScope(new { code, urn }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return NotFound();
                }

                var viewModel = new SchoolRisksViewModel(school);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school risk indicators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("history")]
    public async Task<IActionResult> History(
        string code,
        string urn,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new { code, urn }))
        {
            try
            {
                var school = await schoolApi.SingleAsync(urn).GetResultOrThrow<School>();
                if (school.LACode != code)
                {
                    return NotFound();
                }
                var viewModel = new SchoolRisksHistoryViewModel(school)
                {
                    ViewAs = viewAs
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school risk indicators history: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("history")]
    public IActionResult History(
        string code,
        string urn,
        int? viewAs)
    {
        return RedirectToAction("History", new
        {
            code,
            urn,
            viewAs
        });
    }
}
