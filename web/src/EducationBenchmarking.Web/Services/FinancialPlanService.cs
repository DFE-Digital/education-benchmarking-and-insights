using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.ViewModels;

namespace EducationBenchmarking.Web.Services;

public interface IFinancialPlanService
{
    Task TryCreateInitialPlan(string? urn, int? year);
    Task<FinancialPlan> GetPlan(string? urn, int? year);
    Task UpdateUseFigures(School school, SchoolPlanCreateViewModel model, Finances finances);
    Task UpdateTimetablePeriods(School school, SchoolPlanCreateViewModel model);
    Task UpdateTotalIncome(School school, SchoolPlanCreateViewModel model);
    Task UpdateTotalExpenditure(School school, SchoolPlanCreateViewModel model);
    Task UpdateTotalTeacherCosts(School school, SchoolPlanCreateViewModel model);
    Task UpdateTotalNumberTeachers(School school, SchoolPlanCreateViewModel model);
    Task UpdateTotalEducationSupport(School school, SchoolPlanCreateViewModel model);
    Task UpdatePrimaryHasMixedAgeClasses(School school, SchoolPlanCreateViewModel model);
    Task UpdatePrimaryMixedAgeClasses(School school, SchoolPlanCreateViewModel model);
    Task UpdatePupilFigures(School school, SchoolPlanCreateViewModel model);
    Task UpdatePrimaryPupilFigures(School school, SchoolPlanCreateViewModel model);
    Task UpdateTeacherPeriodAllocation(School school, SchoolPlanCreateViewModel model);
}

public class FinancialPlanService(IBenchmarkApi benchmarkApi) : IFinancialPlanService
{
    public async Task TryCreateInitialPlan(string? urn, int? year)
    {
        var plan = await GetPlanOrDefault(urn, year);
        if (plan == null)
        {
            var request = new PutFinancialPlanRequest { Urn = urn, Year = year };
            await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
        }
    }

    private async Task<FinancialPlan?> GetPlanOrDefault(string? urn, int? year)
    {
        return await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrDefault<FinancialPlan>();
    }

    public async Task<FinancialPlan> GetPlan(string? urn, int? year)
    {
        return await benchmarkApi.GetFinancialPlan(urn, year).GetResultOrThrow<FinancialPlan>();
    }

    public async Task UpdateUseFigures(School school, SchoolPlanCreateViewModel model, Finances finances)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.UseFigures = model.UseFigures;

        if (model.UseFigures is true)
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
    }

    public async Task UpdateTimetablePeriods(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TimetablePeriods = int.TryParse(model.TimetablePeriods, out var val) ? val : null;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTotalIncome(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TotalIncome = model.TotalIncome;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTotalExpenditure(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TotalExpenditure = model.TotalExpenditure;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTotalTeacherCosts(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TotalTeacherCosts = model.TotalTeacherCosts;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTotalNumberTeachers(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TotalNumberOfTeachersFte = model.TotalNumberOfTeachersFte;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTotalEducationSupport(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.EducationSupportStaffCosts = model.EducationSupportStaffCosts;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePrimaryHasMixedAgeClasses(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.HasMixedAgeClasses = model.HasMixedAgeClasses;

        if (model.HasMixedAgeClasses is false)
        {
            request.MixedAgeReceptionYear1 = false;
            request.MixedAgeYear1Year2 = false;
            request.MixedAgeYear2Year3 = false;
            request.MixedAgeYear3Year4 = false;
            request.MixedAgeYear4Year5 = false;
            request.MixedAgeYear5Year6 = false;

            request.PupilsMixedReceptionYear1 = null;
            request.PupilsMixedYear1Year2 = null;
            request.PupilsMixedYear2Year3 = null;
            request.PupilsMixedYear3Year4 = null;
            request.PupilsMixedYear4Year5 = null;
            request.PupilsMixedYear5Year6 = null;

            request.TeachersMixedReceptionYear1 = null;
            request.TeachersMixedYear1Year2 = null;
            request.TeachersMixedYear2Year3 = null;
            request.TeachersMixedYear3Year4 = null;
            request.TeachersMixedYear4Year5 = null;
            request.TeachersMixedYear5Year6 = null;
        }

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePrimaryMixedAgeClasses(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.MixedAgeReceptionYear1 = model.MixedAgeReceptionYear1;
        request.MixedAgeYear1Year2 = model.MixedAgeYear1Year2;
        request.MixedAgeYear2Year3 = model.MixedAgeYear2Year3;
        request.MixedAgeYear3Year4 = model.MixedAgeYear3Year4;
        request.MixedAgeYear4Year5 = model.MixedAgeYear4Year5;
        request.MixedAgeYear5Year6 = model.MixedAgeYear5Year6;

        request.PupilsMixedReceptionYear1 = model.MixedAgeReceptionYear1 ? plan.PupilsMixedReceptionYear1 : null;
        request.PupilsMixedYear1Year2 = model.MixedAgeYear1Year2 ? plan.PupilsMixedYear1Year2 : null;
        request.PupilsMixedYear2Year3 = model.MixedAgeYear2Year3 ? plan.PupilsMixedYear2Year3 : null;
        request.PupilsMixedYear3Year4 = model.MixedAgeYear3Year4 ? plan.PupilsMixedYear3Year4 : null;
        request.PupilsMixedYear4Year5 = model.MixedAgeYear4Year5 ? plan.PupilsMixedYear4Year5 : null;
        request.PupilsMixedYear5Year6 = model.MixedAgeYear5Year6 ? plan.PupilsMixedYear5Year6 : null;

        request.PupilsReception = model.MixedAgeReceptionYear1 ? null : plan.PupilsReception;
        request.PupilsYear1 = model.MixedAgeReceptionYear1 || model.MixedAgeYear1Year2 ? null : plan.PupilsYear1;
        request.PupilsYear2 = model.MixedAgeYear1Year2 || model.MixedAgeYear2Year3 ? null : plan.PupilsYear2;
        request.PupilsYear3 = model.MixedAgeYear2Year3 || model.MixedAgeYear3Year4 ? null : plan.PupilsYear3;
        request.PupilsYear4 = model.MixedAgeYear3Year4 || model.MixedAgeYear4Year5 ? null : plan.PupilsYear4;
        request.PupilsYear5 = model.MixedAgeYear4Year5 || model.MixedAgeYear5Year6 ? null : plan.PupilsYear5;
        request.PupilsYear6 = model.MixedAgeYear5Year6 ? null : plan.PupilsYear6;

        request.TeachersMixedReceptionYear1 = model.MixedAgeReceptionYear1 ? plan.TeachersMixedReceptionYear1 : null;
        request.TeachersMixedYear1Year2 = model.MixedAgeYear1Year2 ? plan.TeachersMixedYear1Year2 : null;
        request.TeachersMixedYear2Year3 = model.MixedAgeYear2Year3 ? plan.TeachersMixedYear2Year3 : null;
        request.TeachersMixedYear3Year4 = model.MixedAgeYear3Year4 ? plan.TeachersMixedYear3Year4 : null;
        request.TeachersMixedYear4Year5 = model.MixedAgeYear4Year5 ? plan.TeachersMixedYear4Year5 : null;
        request.TeachersMixedYear5Year6 = model.MixedAgeYear5Year6 ? plan.TeachersMixedYear5Year6 : null;

        request.TeachersReception = model.MixedAgeReceptionYear1 ? null : plan.TeachersReception;
        request.TeachersYear1 = model.MixedAgeReceptionYear1 || model.MixedAgeYear1Year2 ? null : plan.TeachersYear1;
        request.TeachersYear2 = model.MixedAgeYear1Year2 || model.MixedAgeYear2Year3 ? null : plan.TeachersYear2;
        request.TeachersYear3 = model.MixedAgeYear2Year3 || model.MixedAgeYear3Year4 ? null : plan.TeachersYear3;
        request.TeachersYear4 = model.MixedAgeYear3Year4 || model.MixedAgeYear4Year5 ? null : plan.TeachersYear4;
        request.TeachersYear5 = model.MixedAgeYear4Year5 || model.MixedAgeYear5Year6 ? null : plan.TeachersYear5;
        request.TeachersYear6 = model.MixedAgeYear5Year6 ? null : plan.TeachersYear6;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.PupilsYear7 = model.PupilsYear7.ToInt();
        request.PupilsYear8 = model.PupilsYear8.ToInt();
        request.PupilsYear9 = model.PupilsYear9.ToInt();
        request.PupilsYear10 = model.PupilsYear10.ToInt();
        request.PupilsYear11 = model.PupilsYear11.ToInt();
        request.PupilsYear12 = model.PupilsYear12;
        request.PupilsYear13 = model.PupilsYear13;

        request.TeachersYear7 = model.PupilsYear7.ToInt() > 0 ? request.TeachersYear7 : null;
        request.TeachersYear8 = model.PupilsYear8.ToInt() > 0 ? request.TeachersYear8 : null;
        request.TeachersYear9 = model.PupilsYear9.ToInt() > 0 ? request.TeachersYear9 : null;
        request.TeachersYear10 = model.PupilsYear10.ToInt() > 0 ? request.TeachersYear10 : null;
        request.TeachersYear11 = model.PupilsYear11.ToInt() > 0 ? request.TeachersYear11 : null;
        request.TeachersYear12 = model.PupilsYear12 > 0 ? request.TeachersYear12 : null;
        request.TeachersYear13 = model.PupilsYear13 > 0 ? request.TeachersYear13 : null;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePrimaryPupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.PupilsNursery = model.PupilsNursery;
        request.PupilsMixedReceptionYear1 = model.PupilsMixedReceptionYear1.ToInt();
        request.PupilsMixedYear1Year2 = model.PupilsMixedYear1Year2.ToInt();
        request.PupilsMixedYear2Year3 = model.PupilsMixedYear2Year3.ToInt();
        request.PupilsMixedYear3Year4 = model.PupilsMixedYear3Year4.ToInt();
        request.PupilsMixedYear4Year5 = model.PupilsMixedYear4Year5.ToInt();
        request.PupilsMixedYear5Year6 = model.PupilsMixedYear5Year6.ToInt();
        request.PupilsReception = model.PupilsReception.ToInt();
        request.PupilsYear1 = model.PupilsYear1.ToInt();
        request.PupilsYear2 = model.PupilsYear2.ToInt();
        request.PupilsYear3 = model.PupilsYear3.ToInt();
        request.PupilsYear4 = model.PupilsYear4.ToInt();
        request.PupilsYear5 = model.PupilsYear5.ToInt();
        request.PupilsYear6 = model.PupilsYear6.ToInt();

        request.TeachersNursery = model.PupilsNursery > 0 ? request.TeachersNursery : null;
        request.TeachersMixedReceptionYear1 = model.PupilsMixedReceptionYear1.ToInt() > 0 ? request.TeachersMixedReceptionYear1 : null;
        request.TeachersMixedYear1Year2 = model.PupilsMixedYear1Year2.ToInt() > 0 ? request.TeachersMixedYear1Year2 : null;
        request.TeachersMixedYear2Year3 = model.PupilsMixedYear2Year3.ToInt() > 0 ? request.TeachersMixedYear2Year3 : null;
        request.TeachersMixedYear3Year4 = model.PupilsMixedYear3Year4.ToInt() > 0 ? request.TeachersMixedYear3Year4 : null;
        request.TeachersMixedYear4Year5 = model.PupilsMixedYear4Year5.ToInt() > 0 ? request.TeachersMixedYear4Year5 : null;
        request.TeachersMixedYear5Year6 = model.PupilsMixedYear5Year6.ToInt() > 0 ? request.TeachersMixedYear5Year6 : null;
        request.TeachersReception = model.PupilsReception.ToInt() > 0 ? request.TeachersReception : null;
        request.TeachersYear1 = model.PupilsYear1.ToInt() > 0 ? request.TeachersYear1 : null;
        request.TeachersYear2 = model.PupilsYear2.ToInt() > 0 ? request.TeachersYear2 : null;
        request.TeachersYear3 = model.PupilsYear3.ToInt() > 0 ? request.TeachersYear3 : null;
        request.TeachersYear4 = model.PupilsYear4.ToInt() > 0 ? request.TeachersYear4 : null;
        request.TeachersYear5 = model.PupilsYear5.ToInt() > 0 ? request.TeachersYear5 : null;
        request.TeachersYear6 = model.PupilsYear6.ToInt() > 0 ? request.TeachersYear6 : null;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdateTeacherPeriodAllocation(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.TeachersNursery = model.TeachersNursery.ToInt();
        request.TeachersMixedReceptionYear1 = model.TeachersMixedReceptionYear1.ToInt();
        request.TeachersMixedYear1Year2 = model.TeachersMixedYear1Year2.ToInt();
        request.TeachersMixedYear2Year3 = model.TeachersMixedYear2Year3.ToInt();
        request.TeachersMixedYear3Year4 = model.TeachersMixedYear3Year4.ToInt();
        request.TeachersMixedYear4Year5 = model.TeachersMixedYear4Year5.ToInt();
        request.TeachersMixedYear5Year6 = model.TeachersMixedYear5Year6.ToInt();
        request.TeachersReception = model.TeachersReception.ToInt();
        request.TeachersYear1 = model.TeachersYear1.ToInt();
        request.TeachersYear2 = model.TeachersYear2.ToInt();
        request.TeachersYear3 = model.TeachersYear3.ToInt();
        request.TeachersYear4 = model.TeachersYear4.ToInt();
        request.TeachersYear5 = model.TeachersYear5.ToInt();
        request.TeachersYear6 = model.TeachersYear6.ToInt();
        request.TeachersYear7 = model.TeachersYear7.ToInt();
        request.TeachersYear8 = model.TeachersYear8.ToInt();
        request.TeachersYear9 = model.TeachersYear9.ToInt();
        request.TeachersYear10 = model.TeachersYear10.ToInt();
        request.TeachersYear11 = model.TeachersYear11.ToInt();
        request.TeachersYear12 = model.TeachersYear12.ToInt();
        request.TeachersYear13 = model.TeachersYear13.ToInt();

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }
}