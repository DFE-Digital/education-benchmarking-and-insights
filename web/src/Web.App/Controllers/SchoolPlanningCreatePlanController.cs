using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Domain.FinancialPlanStages;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.Validators;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[SchoolAuthorization]
[FeatureGate(FeatureFlags.CurriculumFinancialPlanning)]
[Route("school/{urn}/financial-planning/create")]
public class SchoolPlanningCreateController(
    IEstablishmentApi establishmentApi,
    IFinancialPlanService financialPlanService,
    IFinanceService financeService,
    IFinancialPlanStageValidator validator,
    ILogger<SchoolPlanningCreateController> logger)
    : Controller
{
    [HttpGet]
    [Route("start")]
    public async Task<IActionResult> Start(string urn)
    {
        return await Executor(new { urn }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = IndexBackLink(urn);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school);

            return View("Start", viewModel);
        }
    }

    [HttpGet]
    [Route("help")]
    public async Task<IActionResult> Help(string urn)
    {
        return await Executor(new { urn }, Action);

        Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = StartBackLink(urn);

            return Task.FromResult<IActionResult>(View("Help"));
        }
    }

    [HttpGet]
    [Route("select-year")]
    public async Task<IActionResult> SelectYear(string urn)
    {
        return await Executor(new { urn }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = StartBackLink(urn);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school);

            return View("SelectYear", viewModel);
        }
    }

    [HttpPost]
    [Route("select-year")]
    public async Task<IActionResult> SelectYear(string urn, SelectYearStage stage)
    {
        return await Executor(new { urn }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.TryCreateEmpty(urn, stage.Year);
                return RedirectToAction("PrePopulateData", new { urn, year = stage.Year });
            }

            ViewData[ViewDataKeys.Backlink] = StartBackLink(urn);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school);

            results.AddToModelState(ModelState);

            return View("SelectYear", viewModel);
        }
    }

    [HttpGet]
    [Route("pre-populate-data")]
    public async Task<IActionResult> PrePopulateData(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = SelectYearBackLink(urn);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var finances = await financeService.GetFinances(urn);
            var viewModel = new SchoolPlanCreateViewModel(school, plan, finances);

            return View("PrePopulateData", viewModel);
        }
    }

    [HttpPost]
    [Route("pre-populate-data")]
    public async Task<IActionResult> PrePopulateData(string urn, int year, PrePopulateDataStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);

                return stage.UseFigures is true
                    ? RedirectToAction("TimetableCycle", new { urn, year })
                    : RedirectToAction("TotalIncome", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = SelectYearBackLink(urn);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var finances = await financeService.GetFinances(urn);
            var viewModel = new SchoolPlanCreateViewModel(school, plan, finances);

            results.AddToModelState(ModelState);

            return View("PrePopulateData", viewModel);
        }
    }

    [HttpGet]
    [Route("timetable-cycle")]
    public async Task<IActionResult> TimetableCycle(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var plan = await financialPlanService.Get(urn, year);
            var backAction = plan.UseFigures is true
                ? PrePopulateDataBackLink(urn, year)
                : TotalNumberTeachersBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TimetableCycle", viewModel);
        }
    }

    [HttpPost]
    [Route("timetable-cycle")]
    public async Task<IActionResult> TimetableCycle(string urn, int year, TimetableCycleStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return school.IsPrimary
                    ? RedirectToAction("PrimaryHasMixedAgeClasses", new { urn, year })
                    : RedirectToAction("PupilFigures", new { urn, year });
            }

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var backAction = plan.UseFigures is true
                ? PrePopulateDataBackLink(urn, year)
                : TotalNumberTeachersBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TimetableCycle", viewModel);
        }
    }

    [HttpGet]
    [Route("total-income")]
    public async Task<IActionResult> TotalIncome(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = PrePopulateDataBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TotalIncome", viewModel);
        }
    }

    [HttpPost]
    [Route("total-income")]
    public async Task<IActionResult> TotalIncome(string urn, int year, TotalIncomeStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TotalExpenditure", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = PrePopulateDataBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TotalIncome", viewModel);
        }
    }

    [HttpGet]
    [Route("total-expenditure")]
    public async Task<IActionResult> TotalExpenditure(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TotalExpenditure", viewModel);
        }
    }

    [HttpPost]
    [Route("total-expenditure")]
    public async Task<IActionResult> TotalExpenditure(string urn, int year, TotalExpenditureStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TotalTeacherCosts", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TotalExpenditure", viewModel);
        }
    }

    [HttpGet]
    [Route("total-teacher-costs")]
    public async Task<IActionResult> TotalTeacherCosts(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TotalExpenditureBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TotalTeacherCosts", viewModel);
        }
    }

    [HttpPost]
    [Route("total-teacher-costs")]
    public async Task<IActionResult> TotalTeacherCosts(string urn, int year, TotalTeacherCostsStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return school.IsPrimary
                    ? RedirectToAction("TotalEducationSupport", new { urn, year })
                    : RedirectToAction("TotalNumberTeachers", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TotalIncomeBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TotalTeacherCosts", viewModel);
        }
    }

    [HttpGet]
    [Route("total-education-support")]
    public async Task<IActionResult> TotalEducationSupport(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TotalTeacherCostsBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TotalEducationSupport", viewModel);
        }
    }

    [HttpPost]
    [Route("total-education-support")]
    public async Task<IActionResult> TotalEducationSupport(string urn, int year, TotalEducationSupportStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TotalNumberTeachers", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TotalTeacherCostsBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TotalEducationSupport", viewModel);
        }
    }

    [HttpGet]
    [Route("total-number-teachers")]
    public async Task<IActionResult> TotalNumberTeachers(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var backAction = school.IsPrimary
                ? TotalEducationSupportBackLink(urn, year)
                : TotalTeacherCostsBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TotalNumberTeachers", viewModel);
        }
    }

    [HttpPost]
    [Route("total-number-teachers")]
    public async Task<IActionResult> TotalNumberTeachers(string urn, int year, TotalNumberTeachersStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TimetableCycle", new { urn, year });
            }

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var backAction = school.IsPrimary
                ? TotalEducationSupportBackLink(urn, year)
                : TotalTeacherCostsBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TotalNumberTeachers", viewModel);
        }
    }

    [HttpGet]
    [Route("primary-has-mixed-age-classes")]
    public async Task<IActionResult> PrimaryHasMixedAgeClasses(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("PrimaryHasMixedAgeClasses", viewModel);
        }
    }

    [HttpPost]
    [Route("primary-has-mixed-age-classes")]
    public async Task<IActionResult> PrimaryHasMixedAgeClasses(string urn, int year, PrimaryHasMixedAgeClassesStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return stage.HasMixedAgeClasses is true
                    ? RedirectToAction("PrimaryMixedAgeClasses", new { urn, year })
                    : RedirectToAction("PrimaryPupilFigures", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("PrimaryHasMixedAgeClasses", viewModel);
        }
    }

    [HttpGet]
    [Route("primary-mixed-age-classes")]
    public async Task<IActionResult> PrimaryMixedAgeClasses(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = PrimaryHasMixedAgeClassesBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("PrimaryMixedAgeClasses", viewModel);
        }
    }

    [HttpPost]
    [Route("primary-mixed-age-classes")]
    public async Task<IActionResult> PrimaryMixedAgeClasses(string urn, int year, PrimaryMixedAgeClassesStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("PrimaryPupilFigures", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = PrimaryHasMixedAgeClassesBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("PrimaryMixedAgeClasses", viewModel);
        }
    }

    [HttpGet]
    [Route("primary-pupil-figures")]
    public async Task<IActionResult> PrimaryPupilFigures(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var plan = await financialPlanService.Get(urn, year);
            var backAction = plan.HasMixedAgeClasses is true
                ? PrimaryMixedAgeClassesBackLink(urn, year)
                : PrimaryHasMixedAgeClassesBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("PrimaryPupilFigures", viewModel);
        }
    }

    [HttpPost]
    [Route("primary-pupil-figures")]
    public async Task<IActionResult> PrimaryPupilFigures(string urn, int year, PrimaryPupilFiguresStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TeacherPeriodAllocation", new { urn, year });
            }

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var backAction = plan.HasMixedAgeClasses is true
                ? PrimaryMixedAgeClassesBackLink(urn, year)
                : PrimaryHasMixedAgeClassesBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("PrimaryPupilFigures", viewModel);
        }
    }

    [HttpGet]
    [Route("pupil-figures")]
    public async Task<IActionResult> PupilFigures(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("PupilFigures", viewModel);
        }
    }

    [HttpPost]
    [Route("pupil-figures")]
    public async Task<IActionResult> PupilFigures(string urn, int year, PupilFiguresStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TeacherPeriodAllocation", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TimetableCycleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("PupilFigures", viewModel);
        }
    }

    [HttpGet]
    [Route("teacher-period-allocation")]
    public async Task<IActionResult> TeacherPeriodAllocation(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var backAction = school.IsPrimary
                ? PrimaryPupilFiguresBackLink(urn, year)
                : PupilFiguresBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TeacherPeriodAllocation", viewModel);
        }
    }

    [HttpPost]
    [Route("teacher-period-allocation")]
    public async Task<IActionResult> TeacherPeriodAllocation(string urn, int year, TeacherPeriodAllocationStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return school.IsPrimary
                    ? RedirectToAction("TeachingAssistantFigures", new { urn, year })
                    : RedirectToAction("OtherTeachingPeriods", new { urn, year });
            }

            var backAction = school.IsPrimary
                ? PrimaryPupilFiguresBackLink(urn, year)
                : PupilFiguresBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TeacherPeriodAllocation", viewModel);
        }
    }

    [HttpGet]
    [Route("teaching-assistant-figures")]
    public async Task<IActionResult> TeachingAssistantFigures(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = TeacherPeriodAllocationBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TeachingAssistantFigures", viewModel);
        }
    }

    [HttpPost]
    [Route("teaching-assistant-figures")]
    public async Task<IActionResult> TeachingAssistantFigures(string urn, int year, TeachingAssistantFiguresStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("OtherTeachingPeriods", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = TeacherPeriodAllocationBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TeachingAssistantFigures", viewModel);
        }
    }

    [HttpGet]
    [Route("other-teaching-periods")]
    public async Task<IActionResult> OtherTeachingPeriods(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var backAction = school.IsPrimary
                ? TeachingAssistantFiguresBackLink(urn, year)
                : TeacherPeriodAllocationBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);
            if (viewModel.Plan?.OtherTeachingPeriods.Count == 0)
            {
                viewModel.Plan.OtherTeachingPeriods.Add(new FinancialPlan.OtherTeachingPeriod());
            }

            return View("OtherTeachingPeriods", viewModel);
        }
    }

    [HttpPost]
    [Route("other-teaching-periods")]
    public async Task<IActionResult> OtherTeachingPeriods(string urn, int year, OtherTeachingPeriodsStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            FormAction action = stage.Action ?? throw new ArgumentNullException(nameof(stage.Action));
            switch (action.Action)
            {
                case FormAction.Add:
                    stage.OtherTeachingPeriods.Add(new OtherTeachingPeriodsStage.OtherTeachingPeriod());
                    break;
                case FormAction.Remove:
                    stage.OtherTeachingPeriods.RemoveAt(int.Parse(action.Identifier ?? "0"));
                    break;
            }

            var results = await validator.ValidateAsync(stage);
            if (results.IsValid && action.Action == FormAction.Continue)
            {
                await financialPlanService.Update(urn, year, stage);
                return stage.OtherTeachingPeriods.All(x => string.IsNullOrEmpty(x.PeriodName))
                    ? RedirectToAction("OtherTeachingPeriodsConfirm", new { urn, year })
                    : RedirectToAction("OtherTeachingPeriodsReview", new { urn, year });
            }

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var backAction = school.IsPrimary
                ? TeachingAssistantFiguresBackLink(urn, year)
                : TeacherPeriodAllocationBackLink(urn, year);

            ViewData[ViewDataKeys.Backlink] = backAction;

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("OtherTeachingPeriods", viewModel);
        }
    }

    [HttpGet]
    [Route("other-teaching-periods-confirmation")]
    public async Task<IActionResult> OtherTeachingPeriodsConfirm(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = OtherTeachingPeriodsBackLink(urn, year);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var plan = await financialPlanService.Get(urn, year);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("OtherTeachingPeriodsConfirm", viewModel);
        }
    }

    [HttpPost]
    [Route("other-teaching-periods-confirmation")]
    public async Task<IActionResult> OtherTeachingPeriodsConfirm(string urn, int year, OtherTeachingPeriodsConfirmStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {

                return stage.Proceed is true
                    ? RedirectToAction("ManagementRoles", new { urn, year })
                    : RedirectToAction("OtherTeachingPeriods", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = OtherTeachingPeriodsBackLink(urn, year);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var plan = await financialPlanService.Get(urn, year);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("OtherTeachingPeriodsConfirm", viewModel);
        }
    }

    [HttpGet]
    [Route("other-teaching-periods-review")]
    public async Task<IActionResult> OtherTeachingPeriodsReview(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = OtherTeachingPeriodsBackLink(urn, year);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var plan = await financialPlanService.Get(urn, year);

            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("OtherTeachingPeriodsReview", viewModel);
        }
    }

    [HttpGet]
    [Route("management-roles")]
    public async Task<IActionResult> ManagementRoles(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = OtherTeachingPeriodsBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("ManagementRoles", viewModel);
        }
    }

    [HttpPost]
    [Route("management-roles")]
    public async Task<IActionResult> ManagementRoles(string urn, int year, ManagementRolesStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("ManagersPerRole", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = OtherTeachingPeriodsBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("ManagementRoles", viewModel);
        }
    }

    [HttpGet]
    [Route("managers-per-role")]
    public async Task<IActionResult> ManagersPerRole(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = ManagementRolesBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("ManagersPerRole", viewModel);
        }
    }

    [HttpPost]
    [Route("managers-per-role")]
    public async Task<IActionResult> ManagersPerRole(string urn, int year, ManagersPerRoleStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("TeachingPeriodsManager", new { urn, year });
            }

            ViewData[ViewDataKeys.Backlink] = ManagementRolesBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("ManagersPerRole", viewModel);
        }
    }

    [HttpGet]
    [Route("teaching-periods-manager")]
    public async Task<IActionResult> TeachingPeriodsManager(string urn, int year)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            ViewData[ViewDataKeys.Backlink] = ManagersPerRoleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            return View("TeachingPeriodsManager", viewModel);
        }
    }

    [HttpPost]
    [Route("teaching-periods-manager")]
    public async Task<IActionResult> TeachingPeriodsManager(string urn, int year, TeachingPeriodsManagerStage stage)
    {
        return await Executor(new { urn, year }, Action);

        async Task<IActionResult> Action()
        {
            var results = await validator.ValidateAsync(stage);
            if (results.IsValid)
            {
                await financialPlanService.Update(urn, year, stage);
                return RedirectToAction("View", "SchoolPlanning", new { urn, year, referrer = Referrers.TeachingPeriodsManager });
            }

            ViewData[ViewDataKeys.Backlink] = ManagersPerRoleBackLink(urn, year);

            var plan = await financialPlanService.Get(urn, year);
            stage.SetPlanValues(plan);

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            var viewModel = new SchoolPlanCreateViewModel(school, plan);

            results.AddToModelState(ModelState);

            return View("TeachingPeriodsManager", viewModel);
        }
    }

    private async Task<IActionResult> Executor(object logState, Func<Task<IActionResult>> action)
    {
        using (logger.BeginScope(logState))
        {
            try
            {
                return await action();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school curriculum and financial planning: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private BacklinkInfo IndexBackLink(string urn) => new(Url.Action("Index", "SchoolPlanning", new { urn }));
    private BacklinkInfo StartBackLink(string urn) => new(Url.Action("Start", new { urn }));
    private BacklinkInfo SelectYearBackLink(string urn) => new(Url.Action("SelectYear", new { urn }));
    private BacklinkInfo PrePopulateDataBackLink(string urn, int year) => new(Url.Action("PrePopulateData", new { urn, year }));
    private BacklinkInfo TotalIncomeBackLink(string urn, int year) => new(Url.Action("TotalIncome", new { urn, year }));
    private BacklinkInfo TotalExpenditureBackLink(string urn, int year) => new(Url.Action("TotalExpenditure", new { urn, year }));
    private BacklinkInfo TotalNumberTeachersBackLink(string urn, int year) => new(Url.Action("TotalNumberTeachers", new { urn, year }));
    private BacklinkInfo TotalEducationSupportBackLink(string urn, int year) => new(Url.Action("TotalEducationSupport", new { urn, year }));
    private BacklinkInfo TotalTeacherCostsBackLink(string urn, int year) => new(Url.Action("TotalTeacherCosts", new { urn, year }));
    private BacklinkInfo TimetableCycleBackLink(string urn, int year) => new(Url.Action("TimetableCycle", new { urn, year }));
    private BacklinkInfo PrimaryHasMixedAgeClassesBackLink(string urn, int year) => new(Url.Action("PrimaryHasMixedAgeClasses", new { urn, year }));
    private BacklinkInfo PrimaryMixedAgeClassesBackLink(string urn, int year) => new(Url.Action("PrimaryMixedAgeClasses", new { urn, year }));
    private BacklinkInfo PupilFiguresBackLink(string urn, int year) => new(Url.Action("PupilFigures", new { urn, year }));
    private BacklinkInfo PrimaryPupilFiguresBackLink(string urn, int year) => new(Url.Action("PrimaryPupilFigures", new { urn, year }));
    private BacklinkInfo TeacherPeriodAllocationBackLink(string urn, int year) => new(Url.Action("TeacherPeriodAllocation", new { urn, year }));
    private BacklinkInfo TeachingAssistantFiguresBackLink(string urn, int year) => new(Url.Action("TeachingAssistantFigures", new { urn, year }));
    private BacklinkInfo OtherTeachingPeriodsBackLink(string urn, int year) => new(Url.Action("OtherTeachingPeriods", new { urn, year }));
    private BacklinkInfo ManagementRolesBackLink(string urn, int year) => new(Url.Action("ManagementRoles", new { urn, year }));
    private BacklinkInfo ManagersPerRoleBackLink(string urn, int year) => new(Url.Action("ManagersPerRole", new { urn, year }));
}