using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}")]
public class SchoolController : Controller
{
    private readonly ILogger<SchoolController> _logger;
    private readonly IEstablishmentApi _establishmentApi;

    public SchoolController(ILogger<SchoolController> logger, IEstablishmentApi establishmentApi)
    {
        _logger = logger;
        _establishmentApi = establishmentApi;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var node = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                
                ViewData["BreadcrumbNode"] = node; 
                
                var school = await _establishmentApi.Get(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolViewModel(school);
                
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