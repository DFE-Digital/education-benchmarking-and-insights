using EducationBenchmarking.Web.Domain;
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

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.PupilsYear7 = int.TryParse(model.PupilsYear7, out var y7Val) ? y7Val : null;
        request.PupilsYear8 = int.TryParse(model.PupilsYear8, out var y8Val) ? y8Val : null;
        request.PupilsYear9 = int.TryParse(model.PupilsYear9, out var y9Val) ? y9Val : null;
        request.PupilsYear10 = int.TryParse(model.PupilsYear10, out var y10Val) ? y10Val : null;
        request.PupilsYear11 = int.TryParse(model.PupilsYear11, out var y11Val) ? y11Val : null;
        request.PupilsYear12 = model.PupilsYear12;
        request.PupilsYear13 = model.PupilsYear13;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }

    public async Task UpdatePrimaryPupilFigures(School school, SchoolPlanCreateViewModel model)
    {
        var plan = await GetPlan(school.Urn, model.Year);
        var request = PutFinancialPlanRequest.Create(plan);

        request.PupilsNursery = model.PupilsNursery;
        request.PupilsMixedReceptionYear1 = int.TryParse(model.PupilsMixedReceptionYear1, out var recY1) ? recY1 : null;
        request.PupilsMixedYear1Year2 = int.TryParse(model.PupilsMixedYear1Year2, out var y1Y2) ? y1Y2 : null;
        request.PupilsMixedYear2Year3 = int.TryParse(model.PupilsMixedYear2Year3, out var y2Y3) ? y2Y3 : null;
        request.PupilsMixedYear3Year4 = int.TryParse(model.PupilsMixedYear3Year4, out var y3Y4) ? y3Y4 : null;
        request.PupilsMixedYear4Year5 = int.TryParse(model.PupilsMixedYear4Year5, out var y4Y5) ? y4Y5 : null;
        request.PupilsMixedYear5Year6 = int.TryParse(model.PupilsMixedYear5Year6, out var y5Y6) ? y5Y6 : null;
        request.PupilsReception = int.TryParse(model.PupilsReception, out var rec) ? rec : null;
        request.PupilsYear1 = int.TryParse(model.PupilsYear1, out var y1) ? y1 : null;
        request.PupilsYear2 = int.TryParse(model.PupilsYear2, out var y2) ? y2 : null;
        request.PupilsYear3 = int.TryParse(model.PupilsYear3, out var y3) ? y3 : null;
        request.PupilsYear4 = int.TryParse(model.PupilsYear4, out var y4) ? y4 : null;
        request.PupilsYear5 = int.TryParse(model.PupilsYear5, out var y5) ? y5 : null;
        request.PupilsYear6 = int.TryParse(model.PupilsYear6, out var y6) ? y6 : null;

        await benchmarkApi.UpsertFinancialPlan(request).EnsureSuccess();
    }
}