using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Moq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class BenchmarkingWebAppClient : ClientBase<Program>,  IClassFixture<BenchmarkingWebAppFactory>
{
    protected readonly Mock<IInsightApi> InsightApi = new();
    protected readonly Mock<IEstablishmentApi> EstablishmentApi = new();
    protected readonly Mock<IBenchmarkApi> BenchmarkApi = new();
    
    protected Fixture Fixture { get; private set; }
    public BenchmarkingWebAppClient(BenchmarkingWebAppFactory factory) : base(factory)
    {
        Fixture = new Fixture();
    }

    protected override void Configure(IServiceCollection services)
    {
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
    
    public BenchmarkingWebAppClient SetupInsightsFromAcademy(School school, Finances finances)
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetAcademyFinances(school.Urn, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        InsightApi.Setup(api => api.GetSchoolsRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Rating>()));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupInsightsFromMaintainedSchool(School school, Finances finances)
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetMaintainedSchoolFinances(school.Urn)).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        InsightApi.Setup(api => api.GetSchoolsRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Rating>()));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupBenchmark()
    {
        BenchmarkApi.Reset();
        BenchmarkApi.Setup(api => api.CreateComparatorSet(It.IsAny<PostBenchmarkSetRequest?>())).ReturnsAsync(ApiResult.Ok());
        BenchmarkApi.Setup(api => api.GetSchoolSizeBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        BenchmarkApi.Setup(api => api.GetFreeSchoolMealBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        return this;
    }
}