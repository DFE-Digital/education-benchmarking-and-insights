using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/workforce")]
public class SchoolWorkforceController(
    IEstablishmentApi establishmentApi,
    ILogger<SchoolWorkforceController> logger,
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
                ViewData[ViewDataConstants.BreadcrumbNode] = BreadcrumbNodes.SchoolWorkforce(urn); 
                
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var years = await financeService.GetYears();
                var viewModel = new SchoolWorkforceViewModel(school, years);
                
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school workforce: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}