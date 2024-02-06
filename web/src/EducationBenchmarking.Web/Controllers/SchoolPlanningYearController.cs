using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/financial-planning/{year:int}")]
public class SchoolPlanningYearController(
    ILogger<SchoolPlanningYearController> logger,
    IFinanceService financeService,
    IEstablishmentApi establishmentApi) 
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                //TODO: Get if exists plan for school / year
                //TODO: Display previous selection value if plan exists
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("SelectYear", "SchoolPlanning", new { urn });

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(school);
                var viewModel = new SchoolPlanViewModel(school, finances, year);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string urn, int year, [FromForm]bool? useFigures)
    {
        using (logger.BeginScope(new { urn, year, useFigures }))
        {
            try
            {
                //TODO: Get school and financial information
                //TODO: Get if exists plan for school / year
                //TODO: If valid PUT plan data
                //TODO: If invalid return error
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("Start", "SchoolPlanning", new { urn });
                
                if (useFigures.HasValue)
                {
                    return useFigures.Value 
                        ? RedirectToAction("Timetable", new { urn, year })
                        : RedirectToAction("TotalIncome", new { urn, year });
                }
                // TODO: amend as required when working on post - below is here just to stop error with CTA
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(school);
                var viewModel = new SchoolPlanViewModel(school, finances, year);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    
    [HttpGet]
    [Route("timetable")]
    public IActionResult Timetable(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                return new OkResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Route("total-income")]
    public IActionResult TotalIncome(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Route("total-expenditure")]
    public IActionResult TotalExpenditure(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Route("total-teacher-cost")]
    public IActionResult TotalTeacherCost(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Route("total-number-teachers")]
    public IActionResult TotalNumberTeachers(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}