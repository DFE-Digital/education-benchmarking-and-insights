using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class BenchmarkingWebAppClient(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : ClientBase<Program>(factory, output), IClassFixture<BenchmarkingWebAppFactory>
{
    protected readonly Mock<IInsightApi> InsightApi = new();
    protected readonly Mock<IEstablishmentApi> EstablishmentApi = new();
    protected readonly Mock<IBenchmarkApi> BenchmarkApi = new();
    
    protected Fixture Fixture { get; } = new();

    protected override void Configure(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSingleton(InsightApi.Object);
        services.AddSingleton(EstablishmentApi.Object);
        services.AddSingleton(BenchmarkApi.Object);
    }

    public BenchmarkingWebAppClient SetupEstablishment(School school)
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(school.Urn)).ReturnsAsync(ApiResult.Ok(school));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithNotFound()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithException()
    {
        EstablishmentApi.Reset();
        EstablishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).Throws(new Exception());
        return this;
    }
    
    public BenchmarkingWebAppClient SetupInsights(School school, Finances finances)
    {
        InsightApi.Reset();
        
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                InsightApi.Setup(api => api.GetAcademyFinances(school.Urn, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(finances));
                break;
            case EstablishmentTypes.Maintained:
                InsightApi.Setup(api => api.GetMaintainedSchoolFinances(school.Urn)).ReturnsAsync(ApiResult.Ok(finances));
                break;
        }

        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        InsightApi.Setup(api => api.GetSchoolsRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Rating>()));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupBenchmark(FinancialPlan? plan = null)
    {
        var schools = Fixture.Build<School>().CreateMany(30).ToArray();
        
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
        
        BenchmarkApi.Setup(api => api.UpsertFinancialPlan(It.IsAny<PutFinancialPlanRequest>())).ReturnsAsync(ApiResult.Ok());
        BenchmarkApi.Setup(api => api.CreateComparatorSet(It.IsAny<PostBenchmarkSetRequest?>())).ReturnsAsync(ApiResult.Ok(new ComparatorSet<School> { TotalResults = schools.Length, Results = schools }));
        BenchmarkApi.Setup(api => api.GetSchoolSizeBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        BenchmarkApi.Setup(api => api.GetFreeSchoolMealBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        return this;
    }
}