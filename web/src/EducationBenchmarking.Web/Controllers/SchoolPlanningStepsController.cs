using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.TagHelpers;
using Microsoft.FeatureManagement.Mvc;


namespace EducationBenchmarking.Web.Controllers;

[Controller]
[FeatureGate(FeatureFlags.CurriculumFinancialPlanning)]
[Route("school/{urn}/financial-planning/steps")]
public class SchoolPlanningStepsController(
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IBenchmarkApi benchmarkApi,
    ILogger<SchoolPlanningStepsController> logger)
    : Controller
{
    [HttpGet]
    [Route("start")]
    public async Task<IActionResult> Start(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] =
                    new BacklinkInfo(Url.Action("Index", "SchoolPlanning", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolPlanViewModel(school);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("select-year")]
    public async Task<IActionResult> SelectYear(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("Start", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolPlanViewModel(school);

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

    [HttpPost]
    [Route("select-year")]
    public async Task<IActionResult> SelectYear(string urn, [FromForm] int? year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                if (year != null && Constants.AvailableYears.Contains(year.Value))
                {
                    var plan = await benchmarkApi.GetFinancialPlan(urn, year.Value).GetResultOrDefault<FinancialPlan>();
                    if (plan == null)
                    {
                        var request = new PutFinancialPlanRequest { Urn = urn, Year = year.Value };
                        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
                    }

                    return RedirectToAction("PrePopulateData", new { urn, year });
                }

                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("Start", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolPlanViewModel(school, year);
                ModelState.AddModelError("year", "Select the academic year you want to plan");

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s
                    ? StatusCode((int)s.Status)
                    : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("help")]
    public IActionResult Help(string urn)
    {
        ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("Start", new { urn }));
        return View();
    }

    [HttpGet]
    [Route("pre-populate-data")]
    public async Task<IActionResult> PrePopulateData(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("SelectYear", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                var finances = await financeService.GetFinances(school);

                var viewModel = new SchoolPlanFinancesViewModel(school, finances, year, plan);

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

    [HttpPost]
    [Route("pre-populate-data")]
    public async Task<IActionResult> PrePopulateData(string urn, int year, [FromForm] bool? useFigures)
    {
        using (logger.BeginScope(new { urn, year, useFigures }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                var finances = await financeService.GetFinances(school);

                plan.UseFigures = useFigures;

                if (!plan.UseFigures.HasValue)
                {
                    ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("SelectYear", new { urn }));
                    var viewModel = new SchoolPlanFinancesViewModel(school, finances, year, plan);
                    ModelState.AddModelError("useFigures", "Select yes if you want to use these figures");
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);

                if (plan.UseFigures.Value)
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

                return plan.UseFigures.Value
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
    public async Task<IActionResult> TotalIncome(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] =
                    new BacklinkInfo(Url.Action("PrePopulateData", new { urn, year }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();

                var viewModel = new SchoolPlanViewModel(school, year, plan);
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

    [HttpPost]
    [Route("total-income")]
    public async Task<IActionResult> TotalIncome(string urn, int year, decimal? totalIncome)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                plan.TotalIncome = totalIncome;

                if (totalIncome is null or < 0)
                {
                    var msg = totalIncome is null
                        ? "Enter your total income"
                        : "Total income must be 0 or more";

                    ModelState.AddModelError(nameof(SchoolPlanViewModel.TotalIncome), msg);
                    ViewData[ViewDataConstants.Backlink] =
                        new BacklinkInfo(Url.Action("PrePopulateData", new { urn, year }));
                    var viewModel = new SchoolPlanViewModel(school, year, plan);
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);
                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();

                return RedirectToAction("TotalExpenditure", new { urn, year });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total income: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s
                    ? StatusCode((int)s.Status)
                    : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("total-expenditure")]
    public async Task<IActionResult> TotalExpenditure(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("TotalIncome", new { urn, year }));
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();

                var viewModel = new SchoolPlanViewModel(school, year, plan);
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

    [HttpPost]
    [Route("total-expenditure")]
    public async Task<IActionResult> TotalExpenditure(string urn, int year, decimal? totalExpenditure)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                plan.TotalExpenditure = totalExpenditure;

                if (totalExpenditure is null or < 0)
                {
                    var msg = totalExpenditure is null
                        ? "Enter your total expenditure"
                        : "Total expenditure must be 0 or more";

                    ModelState.AddModelError(nameof(SchoolPlanViewModel.TotalExpenditure), msg);
                    ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("TotalIncome", new { urn, year }));

                    var viewModel = new SchoolPlanViewModel(school, year, plan);
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);
                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();

                return RedirectToAction("TotalTeacherCosts", new { urn, year });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total expenditure: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("total-teacher-costs")]
    public async Task<IActionResult> TotalTeacherCosts(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] =
                    new BacklinkInfo(Url.Action("TotalExpenditure", new { urn, year }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();

                var viewModel = new SchoolPlanViewModel(school, year, plan);
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

    [HttpPost]
    [Route("total-teacher-costs")]
    public async Task<IActionResult> TotalTeacherCosts(string urn, int year, decimal? totalTeacherCosts)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                plan.TotalTeacherCosts = totalTeacherCosts;

                if (totalTeacherCosts is null or < 0)
                {
                    var msg = totalTeacherCosts is null
                        ? "Enter your total teacher costs"
                        : "Total teacher costs must be 0 or more";

                    ModelState.AddModelError(nameof(SchoolPlanViewModel.TotalTeacherCosts), msg);
                    ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("TotalExpenditure", new { urn, year }));

                    var viewModel = new SchoolPlanViewModel(school, year, plan);
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);
                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();

                return school.IsPrimary
                    ? RedirectToAction("TotalEducationSupport", new { urn, year })
                    : RedirectToAction("TotalNumberTeachers", new { urn, year });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total teacher cost: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("total-number-teachers")]
    public async Task<IActionResult> TotalNumberTeachers(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();

                ViewData[ViewDataConstants.Backlink] = school.IsPrimary
                    ? new BacklinkInfo(Url.Action("TotalEducationSupport", new { urn, year }))
                    : new BacklinkInfo(Url.Action("TotalTeacherCosts", new { urn, year }));

                var viewModel = new SchoolPlanViewModel(school, year, plan);
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

    [HttpPost]
    [Route("total-number-teachers")]
    public async Task<IActionResult> TotalNumberTeachers(string urn, int year, decimal? totalNumberOfTeachersFte)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                plan.TotalNumberOfTeachersFte = totalNumberOfTeachersFte;

                if (totalNumberOfTeachersFte is null or < 1)
                {
                    var msg = totalNumberOfTeachersFte is null
                        ? "Enter your number of full-time equivalent teachers"
                        : "Number of full-time equivalent teachers must be 1 or more";

                    ModelState.AddModelError(nameof(SchoolPlanViewModel.TotalNumberOfTeachersFte), msg);
                    ViewData[ViewDataConstants.Backlink] = school.IsPrimary
                        ? new BacklinkInfo(Url.Action("TotalEducationSupport", new { urn, year }))
                        : new BacklinkInfo(Url.Action("TotalTeacherCosts", new { urn, year }));

                    var viewModel = new SchoolPlanViewModel(school, year, plan);
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);
                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();

                return new OkResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total teacher cost: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("total-education-support")]
    public async Task<IActionResult> TotalEducationSupport(string urn, int year)
    {
        using (logger.BeginScope(new { urn, year, }))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] =
                    new BacklinkInfo(Url.Action("TotalTeacherCosts", new { urn, year }));
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();

                var viewModel = new SchoolPlanViewModel(school, year, plan);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school education support: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("total-education-support")]
    public async Task<IActionResult> TotalEducationSupport(string urn, int year, decimal? educationSupportStaffCosts)
    {
        using (logger.BeginScope(new { urn, year }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var plan = await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
                plan.EducationSupportStaffCosts = educationSupportStaffCosts;

                if (educationSupportStaffCosts is null or < 0)
                {
                    var msg = educationSupportStaffCosts is null
                        ? "Enter your total education support staff costs"
                        : "Total education support staff costs must be 0 or more";

                    ViewData[ViewDataConstants.Backlink] = new BacklinkInfo(Url.Action("TotalTeacherCosts", new { urn, year }));
                    ModelState.AddModelError(nameof(SchoolPlanViewModel.EducationSupportStaffCosts), msg);

                    var viewModel = new SchoolPlanViewModel(school, year, plan);
                    return View(viewModel);
                }

                var request = PutFinancialPlanRequest.Create(plan);
                await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();

                return RedirectToAction("TotalNumberTeachers", new { urn, year });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while processing total education support: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}