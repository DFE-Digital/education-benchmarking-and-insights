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
public class SchoolPlanningController : Controller
{
    private readonly IEstablishmentApi _establishmentApi;
    private readonly ILogger<SchoolPlanningController> _logger;

    public SchoolPlanningController(IEstablishmentApi establishmentApi, ILogger<SchoolPlanningController> logger)
    {
        _establishmentApi = establishmentApi;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (_logger.BeginScope(new { urn }))
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

                var school = await _establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolPlanningViewModel(school);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("help")]
    public IActionResult Help(string urn)
    {
        using (_logger.BeginScope(new { urn }))
        {
            try
            {
                var viewModel = new SchoolPlanningHelpViewModel(urn);
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}