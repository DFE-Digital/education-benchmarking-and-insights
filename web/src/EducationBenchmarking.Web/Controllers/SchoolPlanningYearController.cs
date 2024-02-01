using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/financial-planning/{year:int}")]
public class SchoolPlanningYearController(ILogger<SchoolPlanningYearController> logger) : Controller
{
    [HttpGet]
    public IActionResult Index(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                //TODO: Get school and financial information
                //TODO: Get if exists plan for school / year
                //TODO: Display previous selection value if plan exists
                //TODO: Conditionally display total education support staff costs fpr primary schools
                ViewData["Backlink"] = new BacklinkInfo("SelectYear", "SchoolPlanning", new { urn });
                var school = new School { Urn = urn };
                var viewModel = new SchoolPlanViewModel(school, year);
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
    public IActionResult Index(string urn, int year, [FromForm]bool? useFigures)
    {
        using (logger.BeginScope(new { urn, year, useFigures }))
        {
            try
            {
                //TODO: Get school and financial information
                //TODO: Get if exists plan for school / year
                //TODO: If valid PUT plan data
                //TODO: If invalid return error
                ViewData["Backlink"] = new BacklinkInfo("Start", "SchoolPlanning", new { urn });
                
                if (useFigures.HasValue)
                {
                    return useFigures.Value 
                        ? RedirectToAction("Timetable", new { urn, year })
                        : RedirectToAction("TotalIncome", new { urn, year });
                }
                var school = new School { Urn = urn };
                var viewModel = new SchoolPlanViewModel(school, year);
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