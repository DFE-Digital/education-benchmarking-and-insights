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
    IEstablishmentApi establishmentApi,
    IBenchmarkApi benchmarkApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("SelectYear", "SchoolPlanning", new { urn });

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(school);
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrDefault<FinancialPlan>();

                var viewModel = new SchoolPlanFinancesViewModel(school, finances, year, plan);
                
                return View(viewModel);  
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public async Task<IActionResult> Index(string urn, int year, [FromForm] bool? useFigures)
    {
        using (logger.BeginScope(new { urn, year, useFigures }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("SelectYear", "SchoolPlanning", new { urn });

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(school);
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrDefault<FinancialPlan>();
                
                if (!useFigures.HasValue)
                {
                    var viewModel = new SchoolPlanFinancesViewModel(school, finances, year, plan);
                    ModelState.AddModelError("useFigures", "Select whether to use the above figures in your plan");
                    return View(viewModel);
                }
                
                var request = plan != null
                    ? PutFinancialPlanRequest.Create(plan)
                    : new PutFinancialPlanRequest { Urn = urn, Year = year };

                request.UseFigures = useFigures.Value;
                    
                if (useFigures.Value)
                {
                    request.TotalIncome = finances.TotalIncome;
                    request.TotalExpenditure = finances.TotalExpenditure;
                    request.TotalTeacherCosts = finances.TeachingStaffCosts;
                    request.TotalNumberOfTeachersFte = finances.TotalNumberOfTeachersFte;
                    if (school.IsPrimary)
                    {
                        request.EducationSupportStaffCosts = finances.EducationSupportStaffCosts;
                    }
                }

                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
                    
                return useFigures.Value
                    ? RedirectToAction("Timetable", new { urn, year })
                    : RedirectToAction("TotalIncome", new { urn, year });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
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
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
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
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("Index", "SchoolPlanningYear", new { urn, year });
                var viewModel = new SchoolPlanViewModel(new School { Urn = urn }, year);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());

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
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("TotalIncome", "SchoolPlanningYear", new { urn, year });
                var viewModel = new SchoolPlanViewModel(new School { Urn = urn }, year);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
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
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("TotalExpenditure", "SchoolPlanningYear", new { urn, year });

                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
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
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo("TotalTeacherCost", "SchoolPlanningYear", new { urn, year });

                return View();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    
    [HttpPost]
    [Route("total-income")]
    public IActionResult TotalIncome(string urn, int year, decimal? totalIncome)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
              
                
                return RedirectToAction("TotalExpenditure", "SchoolPlanningYear", new { urn, year });

            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total income: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s 
                    ? StatusCode((int)s.Status) 
                    : StatusCode(500);
            }
        }
    }
    
    
    [HttpPost]
    [Route("total-expenditure")]
    public IActionResult TotalExpenditure(string urn, int year, decimal? totalExpenditure)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
              
                
                return RedirectToAction("TotalTeacherCost", "SchoolPlanningYear", new { urn, year });

            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total expenditure: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s 
                    ? StatusCode((int)s.Status) 
                    : StatusCode(500);
            }
        }
    } 
    
    
    
}

    
    
    
    
    

    
    





