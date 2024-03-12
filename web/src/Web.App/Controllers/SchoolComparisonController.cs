using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers
{
    [Controller]
    [Route("school/{urn}/comparison")]
    public class SchoolComparisonController(
        IEstablishmentApi establishmentApi,
        ILogger<SchoolComparisonController> logger,
        IFinanceService financeService)
        : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index(string urn)
        {
            using (logger.BeginScope(new { urn }))
            {
                try
                {
                    ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparison(urn);

                    var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                    var years = await financeService.GetYears();
                    var viewModel = new SchoolComparisonViewModel(school, years);

                    return View(viewModel);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                    return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
                }
            }
        }
    }
}