using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/comparison")]
public class SchoolComparisonController : Controller
{
    private readonly IEstablishmentApi _establishmentApi;
    private readonly ILogger<SchoolComparisonController> _logger;
    private readonly IFinanceService _financeService;

    public SchoolComparisonController(IEstablishmentApi establishmentApi, ILogger<SchoolComparisonController> logger, IFinanceService financeService)
    {
        _establishmentApi = establishmentApi;
        _logger = logger;
        _financeService = financeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var parentNode = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                var childNode = new MvcBreadcrumbNode("Index", "SchoolComparison", "Compare your costs")
                {
                    RouteValues = new { urn },
                    Parent = parentNode
                };
                
                ViewData["BreadcrumbNode"] = childNode; 
                
                var school = await _establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var years = await _financeService.GetYears();
                var viewModel = new SchoolComparisonViewModel(school, years);
                
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}