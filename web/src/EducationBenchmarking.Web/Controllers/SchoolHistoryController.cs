using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;


namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/history")]
public class SchoolHistoryController(
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IBenchmarkApi benchmarkApi,
    ILogger<SchoolPlanningController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] =
                    ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "School", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolHistoryViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }


}