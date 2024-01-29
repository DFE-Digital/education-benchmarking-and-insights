using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/financial-planning")]
public class SchoolPlanningController(IEstablishmentApi establishmentApi, ILogger<SchoolPlanningController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                var childNode = new MvcBreadcrumbNode("Index", "SchoolPlanning", "Curriculum and financial planning")
                {
                    RouteValues = new { urn },
                    Parent = parentNode
                };

                ViewData["BreadcrumbNode"] = childNode;

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolPlanningViewModel(school);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("help")]
    public IActionResult Help(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData["Backlink"] = new TagHelpers.BacklinkInfo("Index", "SchoolPlanning", new { urn });

                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school financial planning help: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}