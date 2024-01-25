using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}")]
public class SchoolController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new {urn}))
        {
            try
            {
                var node = new MvcBreadcrumbNode("Index", "School", "Your school") { RouteValues = new { urn } };
                
                ViewData["BreadcrumbNode"] = node; 
                
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var (finances,ratings) = await financeService.GetRatings(school);
                
                var viewModel = new SchoolViewModel(school, finances, ratings);
                
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