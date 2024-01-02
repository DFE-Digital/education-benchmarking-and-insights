using AutoFixture;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Services;
using Microsoft.AspNetCore.Mvc.Testing;
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
    
    public BenchmarkingWebAppClient SetupInsightsFromAcademy(School school, Finances finances)
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetAcademyFinances(school.Urn, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupInsightsFromMaintainedSchool(School school, Finances finances)
    {
        InsightApi.Reset();
        InsightApi.Setup(api => api.GetMaintainedSchoolFinances(school.Urn)).ReturnsAsync(ApiResult.Ok(finances));
        InsightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        InsightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupBenchmark()
    {
        BenchmarkApi.Reset();
        BenchmarkApi.Setup(api => api.CreateComparatorSet(It.IsAny<PostBenchmarkSetRequest?>())).ReturnsAsync(ApiResult.Ok());
        return this;
    }
}