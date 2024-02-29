using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.Validators;
using EducationBenchmarking.Web.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[FeatureGate(FeatureFlags.CurriculumFinancialPlanning)]
[Route("school/{urn}/financial-planning/create")]
public class SchoolPlanningCreateController(
    IEstablishmentApi establishmentApi,
    IFinancialPlanService financialPlanService,
    IFinanceService financeService,
    IValidator<SchoolPlanCreateViewModel> validator,
    ILogger<SchoolPlanningCreateController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> CreatePlan(string urn, int? year, string step)
    {
        using (logger.BeginScope(new { urn, year, step }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();

                return step switch
                {
                    PlanSteps.Start => GetStart(school),
                    PlanSteps.Help => GetHelp(school),
                    PlanSteps.SelectYear => GetSelectYear(school),
                    PlanSteps.PrePopulateData => await GetPrePopulateData(school, year),
                    PlanSteps.TimetableCycle => await GetTimetableCycle(school, year),
                    PlanSteps.TotalIncome => await GetTotalIncome(school, year),
                    PlanSteps.TotalExpenditure => await GetTotalExpenditure(school, year),
                    PlanSteps.TotalTeacherCosts => await GetTotalTeacherCosts(school, year),
                    PlanSteps.TotalNumberTeachers => await GetTotalNumberTeachers(school, year),
                    PlanSteps.TotalEducationSupport => await GetTotalEducationSupport(school, year),
                    PlanSteps.PrimaryHasMixedAgeClasses => await GetPrimaryHasMixedAgeClasses(school, year),
                    PlanSteps.PrimaryMixedAgeClasses => await GetPrimaryMixedAgeClasses(school, year),
                    PlanSteps.PupilFigures => await GetPupilFigures(school, year),
                    PlanSteps.PrimaryPupilFigures => await GetPrimaryPupilFigures(school, year),
                    PlanSteps.TeacherPeriodAllocation => await GetTeacherPeriodAllocation(school, year),
                    PlanSteps.OtherTeachingPeriods => await GetOtherTeachingPeriods(school, year),
                    _ => throw new ArgumentOutOfRangeException(nameof(step))
                };
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
    public async Task<IActionResult> CreatePlan(string urn, string step, SchoolPlanCreateViewModel model)
    {
        using (logger.BeginScope(new { urn, model, step }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();

                return step switch
                {
                    PlanSteps.SelectYear => await PostSelectYear(school, model),
                    PlanSteps.PrePopulateData => await PostPrePopulateData(school, model),
                    PlanSteps.TimetableCycle => await PostTimetableCycle(school, model),
                    PlanSteps.TotalIncome => await PostTotalIncome(school, model),
                    PlanSteps.TotalExpenditure => await PostTotalExpenditure(school, model),
                    PlanSteps.TotalTeacherCosts => await PostTotalTeacherCosts(school, model),
                    PlanSteps.TotalNumberTeachers => await PostTotalNumberTeachers(school, model),
                    PlanSteps.TotalEducationSupport => await PostTotalEducationSupport(school, model),
                    PlanSteps.PrimaryHasMixedAgeClasses => await PostPrimaryHasMixedAgeClasses(school, model),
                    PlanSteps.PrimaryMixedAgeClasses => await PostPrimaryMixedAgeClasses(school, model),
                    PlanSteps.PupilFigures => await PostPupilFigures(school, model),
                    PlanSteps.PrimaryPupilFigures => await PostPrimaryPupilFigures(school, model),
                    PlanSteps.TeacherPeriodAllocation => await PostTeacherPeriodAllocation(school, model),
                    PlanSteps.OtherTeachingPeriods => await PostOtherTeachingPeriods(school, model),
                    _ => throw new ArgumentOutOfRangeException(nameof(step))
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private ViewResult GetStart(School school)
    {
        ViewData[ViewDataKeys.Backlink] = IndexBackLink(school);
        var viewModel = new SchoolPlanCreateViewModel(school);

        return View("Start", viewModel);
    }

    private ViewResult GetHelp(School school)
    {
        ViewData[ViewDataKeys.Backlink] = StartBackLink(school);
        return View("Help");
    }

    private ViewResult GetSelectYear(School school)
    {
        ViewData[ViewDataKeys.Backlink] = StartBackLink(school);
        var viewModel = new SchoolPlanCreateViewModel(school);
        return View("SelectYear", viewModel);
    }

    private async Task<IActionResult> PostSelectYear(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.SelectYear);
        if (results.IsValid)
        {
            await financialPlanService.TryCreateInitialPlan(school.Urn, model.Year);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.PrePopulateData });
        }

        ViewData[ViewDataKeys.Backlink] = StartBackLink(school);

        var viewModel = new SchoolPlanCreateViewModel(school, model.Year);
        results.AddToModelState(ModelState);

        return View("SelectYear", viewModel);
    }

    private async Task<ViewResult> GetPrePopulateData(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = SelectYearBackLink(school);

        var plan = await financialPlanService.GetPlan(school.Urn, year);
        var finances = await financeService.GetFinances(school);
        var viewModel = new SchoolPlanCreateFinancesViewModel(school, plan, finances);

        return View("PrePopulateData", viewModel);
    }

    private async Task<IActionResult> PostPrePopulateData(School school, SchoolPlanCreateViewModel model)
    {
        var finances = await financeService.GetFinances(school);
        var results = await validator.ValidateAsync(model, Strategy.PrePopulateData);
        if (results.IsValid)
        {
            await financialPlanService.UpdateUseFigures(school, model, finances);

            return model.UseFigures is true
                ? RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TimetableCycle })
                : RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalIncome });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = SelectYearBackLink(school);

        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateFinancesViewModel(school, plan, finances)
        {
            UseFigures = model.UseFigures
        };

        return View("PrePopulateData", viewModel);
    }

    private async Task<ViewResult> GetTimetableCycle(School school, int? year)
    {
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var backAction = plan.UseFigures is true
            ? PrePopulateDataBackLink(school, year)
            : TotalNumberTeachersBackLink(school, year);

        ViewData[ViewDataKeys.Backlink] = backAction;

        var viewModel = new SchoolPlanCreateViewModel(school, plan);

        return View("TimetableCycle", viewModel);
    }

    private async Task<IActionResult> PostTimetableCycle(School school, SchoolPlanCreateViewModel model)
    {


        var results = await validator.ValidateAsync(model, Strategy.TimetableCycle);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTimetablePeriods(school, model);
            return school.IsPrimary
                ? RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.PrimaryHasMixedAgeClasses })
                : RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.PupilFigures });
        }

        results.AddToModelState(ModelState);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var backAction = plan.UseFigures is true
            ? PrePopulateDataBackLink(school, model.Year)
            : TotalNumberTeachersBackLink(school, model.Year);

        ViewData[ViewDataKeys.Backlink] = backAction;
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TimetablePeriods = model.TimetablePeriods
        };

        return View("TimetableCycle", viewModel);
    }

    private async Task<IActionResult> GetTotalIncome(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = PrePopulateDataBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TotalIncome", viewModel);
    }

    private async Task<IActionResult> PostTotalIncome(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TotalIncome);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTotalIncome(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalExpenditure });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = PrePopulateDataBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TotalIncome = model.TotalIncome
        };

        return View("TotalIncome", viewModel);
    }

    private async Task<IActionResult> GetTotalExpenditure(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TotalExpenditure", viewModel);
    }

    private async Task<IActionResult> PostTotalExpenditure(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TotalExpenditure);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTotalExpenditure(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalTeacherCosts });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TotalExpenditure = model.TotalExpenditure
        };

        return View("TotalExpenditure", viewModel);
    }

    private async Task<IActionResult> GetTotalTeacherCosts(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = TotalExpenditureBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TotalTeacherCosts", viewModel);
    }

    private async Task<IActionResult> PostTotalTeacherCosts(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TotalTeacherCosts);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTotalTeacherCosts(school, model);
            return school.IsPrimary
                ? RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalEducationSupport })
                : RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalNumberTeachers });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TotalTeacherCosts = model.TotalTeacherCosts
        };

        return View("TotalTeacherCosts", viewModel);
    }

    private async Task<IActionResult> GetTotalNumberTeachers(School school, int? year)
    {
        var backAction = school.IsPrimary
            ? TotalEducationSupportBackLink(school, year)
            : TotalTeacherCostsBackLink(school, year);

        ViewData[ViewDataKeys.Backlink] = backAction;
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TotalNumberTeachers", viewModel);
    }

    private async Task<IActionResult> PostTotalNumberTeachers(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TotalNumberTeachers);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTotalNumberTeachers(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TimetableCycle });
        }

        results.AddToModelState(ModelState);

        var backAction = school.IsPrimary
            ? TotalEducationSupportBackLink(school, model.Year)
            : TotalTeacherCostsBackLink(school, model.Year);

        ViewData[ViewDataKeys.Backlink] = backAction;
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TotalNumberOfTeachersFte = model.TotalNumberOfTeachersFte
        };

        return View("TotalNumberTeachers", viewModel);
    }

    private async Task<IActionResult> GetTotalEducationSupport(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = TotalTeacherCostsBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TotalEducationSupport", viewModel);
    }

    private async Task<IActionResult> PostTotalEducationSupport(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TotalEducationSupport);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTotalEducationSupport(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TotalNumberTeachers });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TotalTeacherCostsBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            EducationSupportStaffCosts = model.EducationSupportStaffCosts
        };

        return View("TotalEducationSupport", viewModel);
    }

    private async Task<IActionResult> GetPrimaryHasMixedAgeClasses(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("PrimaryHasMixedAgeClasses", viewModel);
    }

    private async Task<IActionResult> PostPrimaryHasMixedAgeClasses(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.PrimaryHasMixedAgeClasses);
        if (results.IsValid)
        {
            await financialPlanService.UpdatePrimaryHasMixedAgeClasses(school, model);
            return model.HasMixedAgeClasses is true
                ? RedirectToAction("CreatePlan",
                    new { urn = school.Urn, year = model.Year, step = PlanSteps.PrimaryMixedAgeClasses })
                : RedirectToAction("CreatePlan",
                    new { urn = school.Urn, year = model.Year, step = PlanSteps.PrimaryPupilFigures });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            HasMixedAgeClasses = model.HasMixedAgeClasses
        };

        return View("PrimaryHasMixedAgeClasses", viewModel);
    }

    private async Task<IActionResult> GetPrimaryMixedAgeClasses(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = PrimaryHasMixedAgeClassesBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("PrimaryMixedAgeClasses", viewModel);
    }

    private async Task<IActionResult> PostPrimaryMixedAgeClasses(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.PrimaryMixedAgeClasses);
        if (results.IsValid)
        {
            await financialPlanService.UpdatePrimaryMixedAgeClasses(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.PrimaryPupilFigures });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = PrimaryHasMixedAgeClassesBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            MixedAgeReceptionYear1 = model.MixedAgeReceptionYear1,
            MixedAgeYear1Year2 = model.MixedAgeYear1Year2,
            MixedAgeYear2Year3 = model.MixedAgeYear2Year3,
            MixedAgeYear3Year4 = model.MixedAgeYear3Year4,
            MixedAgeYear4Year5 = model.MixedAgeYear4Year5,
            MixedAgeYear5Year6 = model.MixedAgeYear5Year6
        };

        return View("PrimaryMixedAgeClasses", viewModel);
    }

    private async Task<IActionResult> GetPupilFigures(School school, int? year)
    {
        ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(school, year);
        var plan = await financialPlanService.GetPlan(school.Urn, year);

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("PupilFigures", viewModel);
    }

    private async Task<IActionResult> PostPupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.PupilFigures);
        if (results.IsValid)
        {
            await financialPlanService.UpdatePupilFigures(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TeacherPeriodAllocation });
        }

        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            PupilsYear7 = model.PupilsYear7,
            PupilsYear8 = model.PupilsYear8,
            PupilsYear9 = model.PupilsYear9,
            PupilsYear10 = model.PupilsYear10,
            PupilsYear11 = model.PupilsYear11,
            PupilsYear12 = model.PupilsYear12,
            PupilsYear13 = model.PupilsYear13
        };

        return View("PupilFigures", viewModel);
    }

    private async Task<IActionResult> GetPrimaryPupilFigures(School school, int? year)
    {
        var plan = await financialPlanService.GetPlan(school.Urn, year);
        var backAction = plan.HasMixedAgeClasses is true
            ? PrimaryMixedAgeClassesBackLink(school, year)
            : PrimaryHasMixedAgeClassesBackLink(school, year);

        ViewData[ViewDataKeys.Backlink] = backAction;

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("PrimaryPupilFigures", viewModel);
    }

    private async Task<IActionResult> PostPrimaryPupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.PrimaryPupilFigures);
        if (results.IsValid)
        {
            await financialPlanService.UpdatePrimaryPupilFigures(school, model);
            return RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.TeacherPeriodAllocation });
        }

        results.AddToModelState(ModelState);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var backAction = plan.HasMixedAgeClasses is true
            ? PrimaryMixedAgeClassesBackLink(school, model.Year)
            : PrimaryHasMixedAgeClassesBackLink(school, model.Year);

        ViewData[ViewDataKeys.Backlink] = backAction;

        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            PupilsNursery = model.PupilsNursery,
            PupilsMixedReceptionYear1 = model.PupilsMixedReceptionYear1,
            PupilsMixedYear1Year2 = model.PupilsMixedYear1Year2,
            PupilsMixedYear2Year3 = model.PupilsMixedYear2Year3,
            PupilsMixedYear3Year4 = model.PupilsMixedYear3Year4,
            PupilsMixedYear4Year5 = model.PupilsMixedYear4Year5,
            PupilsMixedYear5Year6 = model.PupilsMixedYear5Year6,
            PupilsReception = model.PupilsReception,
            PupilsYear1 = model.PupilsYear1,
            PupilsYear2 = model.PupilsYear2,
            PupilsYear3 = model.PupilsYear3,
            PupilsYear4 = model.PupilsYear4,
            PupilsYear5 = model.PupilsYear5,
            PupilsYear6 = model.PupilsYear6
        };

        return View("PrimaryPupilFigures", viewModel);
    }

    private async Task<IActionResult> GetTeacherPeriodAllocation(School school, int? year)
    {
        var plan = await financialPlanService.GetPlan(school.Urn, year);
        var backAction = school.IsPrimary
            ? PrimaryPupilFiguresBackLink(school, year)
            : PupilFiguresBackLink(school, year);

        ViewData[ViewDataKeys.Backlink] = backAction;

        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        return View("TeacherPeriodAllocation", viewModel);
    }


    private async Task<IActionResult> PostTeacherPeriodAllocation(School school, SchoolPlanCreateViewModel model)
    {
        var results = await validator.ValidateAsync(model, Strategy.TeacherPeriodAllocation);
        if (results.IsValid)
        {
            await financialPlanService.UpdateTeacherPeriodAllocation(school, model);
            return school.IsPrimary
                ? new OkResult()
                : RedirectToAction("CreatePlan", new { urn = school.Urn, year = model.Year, step = PlanSteps.OtherTeachingPeriods });
        }

        results.AddToModelState(ModelState);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var backAction = school.IsPrimary
            ? PrimaryPupilFiguresBackLink(school, model.Year)
            : PupilFiguresBackLink(school, model.Year);

        ViewData[ViewDataKeys.Backlink] = backAction;

        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            TeachersNursery = model.TeachersNursery,
            TeachersMixedReceptionYear1 = model.TeachersMixedReceptionYear1,
            TeachersMixedYear1Year2 = model.TeachersMixedYear1Year2,
            TeachersMixedYear2Year3 = model.TeachersMixedYear2Year3,
            TeachersMixedYear3Year4 = model.TeachersMixedYear3Year4,
            TeachersMixedYear4Year5 = model.TeachersMixedYear4Year5,
            TeachersMixedYear5Year6 = model.TeachersMixedYear5Year6,
            TeachersReception = model.TeachersReception,
            TeachersYear1 = model.TeachersYear1,
            TeachersYear2 = model.TeachersYear2,
            TeachersYear3 = model.TeachersYear3,
            TeachersYear4 = model.TeachersYear4,
            TeachersYear5 = model.TeachersYear5,
            TeachersYear6 = model.TeachersYear6,
            TeachersYear7 = model.TeachersYear7,
            TeachersYear8 = model.TeachersYear8,
            TeachersYear9 = model.TeachersYear9,
            TeachersYear10 = model.TeachersYear10,
            TeachersYear11 = model.TeachersYear11,
            TeachersYear12 = model.TeachersYear12,
            TeachersYear13 = model.TeachersYear13
        };

        return View("TeacherPeriodAllocation", viewModel);
    }

    private async Task<IActionResult> GetOtherTeachingPeriods(School school, int? year)
    {
        var plan = await financialPlanService.GetPlan(school.Urn, year);
        ViewData[ViewDataKeys.Backlink] = TeacherPeriodAllocationBackLink(school, year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan);
        if (viewModel.OtherTeachingPeriods.Count == 0)
        {
            viewModel.OtherTeachingPeriods.Add(new SchoolPlanOtherTeachingPeriodsViewModel());
        }
        return View("OtherTeachingPeriods", viewModel);
    }

    private async Task<IActionResult> PostOtherTeachingPeriods(School school, SchoolPlanCreateViewModel model)
    {
        FormAction action = model.Action;
        switch (action.Action)
        {
            case FormAction.Add:
                model.OtherTeachingPeriods.Add(new SchoolPlanOtherTeachingPeriodsViewModel());
                break;
            case FormAction.Remove:
                model.OtherTeachingPeriods.RemoveAt(int.Parse(action.Identifier ?? "0"));
                break;
        }
        
        var results = await validator.ValidateAsync(model, Strategy.OtherTeachingPeriods);
        if (results.IsValid && action.Action == FormAction.Continue)
        {
            return new OkResult();
        }
        
        results.AddToModelState(ModelState);
        ViewData[ViewDataKeys.Backlink] = TeacherPeriodAllocationBackLink(school, model.Year);
        var plan = await financialPlanService.GetPlan(school.Urn, model.Year);
        var viewModel = new SchoolPlanCreateViewModel(school, plan)
        {
            OtherTeachingPeriods = model.OtherTeachingPeriods
        };

        return View("OtherTeachingPeriods", viewModel);
    }

    private BacklinkInfo IndexBackLink(School school) =>
        new(Url.Action("Index", "SchoolPlanning", new { urn = school.Urn }));

    private BacklinkInfo StartBackLink(School school) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, step = PlanSteps.Start }));

    private BacklinkInfo SelectYearBackLink(School school) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, step = PlanSteps.SelectYear }));

    private BacklinkInfo PrePopulateDataBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.PrePopulateData }));

    private BacklinkInfo TotalIncomeBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TotalIncome }));

    private BacklinkInfo TotalExpenditureBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TotalExpenditure }));

    private BacklinkInfo TotalNumberTeachersBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TotalNumberTeachers }));

    private BacklinkInfo TotalEducationSupportBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TotalEducationSupport }));

    private BacklinkInfo TotalTeacherCostsBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TotalTeacherCosts }));

    private BacklinkInfo TimetableCycleBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TimetableCycle }));

    private BacklinkInfo PrimaryHasMixedAgeClassesBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.PrimaryHasMixedAgeClasses }));

    private BacklinkInfo PrimaryMixedAgeClassesBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.PrimaryMixedAgeClasses }));

    private BacklinkInfo PupilFiguresBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.PupilFigures }));

    private BacklinkInfo PrimaryPupilFiguresBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.PrimaryPupilFigures }));

    private BacklinkInfo TeacherPeriodAllocationBackLink(School school, int? year) =>
        new(Url.Action("CreatePlan", new { urn = school.Urn, year, step = PlanSteps.TeacherPeriodAllocation }));
}