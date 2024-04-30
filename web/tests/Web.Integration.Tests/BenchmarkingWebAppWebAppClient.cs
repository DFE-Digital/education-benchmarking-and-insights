using Moq;
using Web.App;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Xunit.Abstractions;

namespace Web.Integration.Tests;


public class SchoolBenchmarkingWebAppClient : BenchmarkingWebAppClient
{
    public SchoolBenchmarkingWebAppClient(IMessageSink messageSink) : base(messageSink, auth =>
            {
                auth.URN = 12345;
            })
    {
    }
}


public abstract class BenchmarkingWebAppClient(IMessageSink messageSink, Action<TestAuthOptions>? authCfg = null) : WebAppClientBase<Program>(messageSink, authCfg)
{
    public Mock<IInsightApi> InsightApi { get; } = new();
    public Mock<IEstablishmentApi> EstablishmentApi { get; } = new();
    public Mock<IBenchmarkApi> BenchmarkApi { get; } = new();
    public Mock<IWorkforceApi> WorkforceApi { get; } = new();

    protected override void Configure(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSingleton(InsightApi.Object);
        services.AddSingleton(EstablishmentApi.Object);
        services.AddSingleton(BenchmarkApi.Object);
        services.AddSingleton(WorkforceApi.Object);
    }

    public BenchmarkingWebAppClient SetupEstablishment(School school)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(school.Urn)).ReturnsAsync(ApiResult.Ok(school));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishment(Trust trust)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetTrust(trust.CompanyNumber)).ReturnsAsync(ApiResult.Ok(trust));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithNotFound()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithException()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.GetTrust(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestSchools(It.IsAny<string>())).Throws(new Exception());
        EstablishmentApi.Setup(api => api.SuggestTrusts(It.IsAny<string>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupWorkforceWithException()
    {
        WorkforceApi.Reset();
        WorkforceApi.Setup(api => api.History(It.IsAny<string?>(), It.IsAny<ApiQuery?>())).Throws(new Exception());
        WorkforceApi.Setup(api => api.Query(It.IsAny<ApiQuery?>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupInsights(School school, Finances finances)
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetSchoolFinances(school.Urn)).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetCurrentReturnYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Aar = 2022, Cfr = 2021 }));
        InsightApi.Setup(api => api.GetRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<RagRating>()));
        return this;
    }

    public BenchmarkingWebAppClient SetupInsights()
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetSchoolFinances(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<RagRating>()));
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmarkWithException()
    {
        BenchmarkApi.Reset();
        BenchmarkApi.Setup(api => api.GetComparatorSet(It.IsAny<string?>())).Throws(new Exception());
        BenchmarkApi.Setup(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>())).Throws(new Exception());
        BenchmarkApi.Setup(api => api.GetFinancialPlan(It.IsAny<string>(), It.IsAny<int>())).Throws(new Exception());
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmarkWithNotFound()
    {
        BenchmarkApi.Reset();
        BenchmarkApi.Setup(api => api.GetFinancialPlan(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(ApiResult.NotFound());
        return this;
    }

    public BenchmarkingWebAppClient SetupBenchmark(School[] schools, FinancialPlanInput? plan = null)
    {
        BenchmarkApi.Reset();
        if (plan == null)
        {
            BenchmarkApi.Setup(api => api.GetFinancialPlan(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(ApiResult.NotFound());
        }
        else
        {
            ArgumentNullException.ThrowIfNull(plan.Urn);
            BenchmarkApi.Setup(api => api.GetFinancialPlan(plan.Urn, plan.Year)).ReturnsAsync(ApiResult.Ok(plan));
        }

        BenchmarkApi
            .Setup(api => api.QueryFinancialPlan(It.IsAny<string>(), It.IsAny<ApiQuery?>()))
            .ReturnsAsync(ApiResult.Ok(Array.Empty<FinancialPlanInput>()));

        BenchmarkApi
            .Setup(api => api.GetComparatorSet(It.IsAny<string?>()))
            .ReturnsAsync(ApiResult.Ok(new ComparatorSet { DefaultArea = schools.Select(x => x.Urn ?? "Missing urn"), DefaultPupil = schools.Select(x => x.Urn ?? "Missing urn") }));

        BenchmarkApi
            .Setup(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>()))
            .ReturnsAsync(ApiResult.Ok())
            .Callback<PutFinancialPlanRequest>(request =>
                BenchmarkApi.Setup(api => api.GetFinancialPlan(request.Urn ?? "", request.Year))
                    .ReturnsAsync(ApiResult.Ok(new FinancialPlanInput
                    {
                        Urn = request.Urn,
                        Year = request.Year.GetValueOrDefault(),
                        UseFigures = request.UseFigures,
                        EducationSupportStaffCosts = request.EducationSupportStaffCosts.ToString(),
                        TotalIncome = request.TotalIncome.ToString(),
                        TotalExpenditure = request.TotalExpenditure.ToString(),
                        TotalTeacherCosts = request.TotalTeacherCosts.ToString(),
                        TotalNumberOfTeachersFte = request.TotalNumberOfTeachersFte,
                        HasMixedAgeClasses = request.HasMixedAgeClasses,
                        MixedAgeReceptionYear1 = request.MixedAgeReceptionYear1,
                        MixedAgeYear1Year2 = request.MixedAgeYear1Year2,
                        MixedAgeYear2Year3 = request.MixedAgeYear2Year3,
                        MixedAgeYear3Year4 = request.MixedAgeYear3Year4,
                        MixedAgeYear4Year5 = request.MixedAgeYear4Year5,
                        MixedAgeYear5Year6 = request.MixedAgeYear5Year6,
                        TimetablePeriods = request.TimetablePeriods.ToString()
                    })));
        return this;
    }
}