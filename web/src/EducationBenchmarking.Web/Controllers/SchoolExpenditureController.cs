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
[Route("school/{urn}/expenditure")]
public class SchoolExpenditureController : Controller
{
    private readonly IEstablishmentApi _establishmentApi;
    private readonly ILogger<SchoolExpenditureController> _logger;
    private readonly IFinanceService _financeService;

    public SchoolExpenditureController(IEstablishmentApi establishmentApi, ILogger<SchoolExpenditureController> logger, IFinanceService financeService)
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
                var childNode = new MvcBreadcrumbNode("Index", "SchoolExpenditure", "Compare your costs")
                {
                    RouteValues = new { urn },
                    Parent = parentNode
                };
                
                ViewData["BreadcrumbNode"] = childNode; 
                
                var school = await _establishmentApi.Get(urn).GetResultOrThrow<School>();
                var finances = await _financeService.GetFinances(school).GetResultOrThrow<Finances>();
                var viewModel = new SchoolExpenditureViewModel(school, finances);
                
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