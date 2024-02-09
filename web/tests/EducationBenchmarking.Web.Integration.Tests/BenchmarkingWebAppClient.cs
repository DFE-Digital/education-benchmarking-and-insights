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
    private readonly Mock<IInsightApi> _insightApi = new();
    private readonly Mock<IEstablishmentApi> _establishmentApi = new();
    private readonly Mock<IBenchmarkApi> _benchmarkApi = new();
    
    protected Fixture Fixture { get; } = new();

    protected override void Configure(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        services.AddSingleton(_insightApi.Object);
        services.AddSingleton(_establishmentApi.Object);
        services.AddSingleton(_benchmarkApi.Object);
    }

    public BenchmarkingWebAppClient SetupEstablishment(School school)
    {
        _establishmentApi.Reset();
        _establishmentApi.Setup(api => api.GetSchool(school.Urn)).ReturnsAsync(ApiResult.Ok(school));
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithNotFound()
    {
        _establishmentApi.Reset();
        _establishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).ReturnsAsync(ApiResult.NotFound);
        return this;
    }

    public BenchmarkingWebAppClient SetupEstablishmentWithException()
    {
        _establishmentApi.Reset();
        _establishmentApi.Setup(api => api.GetSchool(It.IsAny<string>())).Throws(new Exception());
        return this;
    }
    
    public BenchmarkingWebAppClient SetupInsights(School school, Finances finances)
    {
        _insightApi.Reset();
        
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                _insightApi.Setup(api => api.GetAcademyFinances(school.Urn, It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(finances));
                break;
            case EstablishmentTypes.Maintained:
                _insightApi.Setup(api => api.GetMaintainedSchoolFinances(school.Urn)).ReturnsAsync(ApiResult.Ok(finances));
                break;
        }

        _insightApi.Setup(api => api.GetSchoolsExpenditure(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok());
        _insightApi.Setup(api => api.GetFinanceYears()).ReturnsAsync(ApiResult.Ok(new FinanceYears { Academies = 2022, MaintainedSchools = 2021}));
        _insightApi.Setup(api => api.GetSchoolsRatings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Rating>()));
        return this;
    }
    
    public BenchmarkingWebAppClient SetupBenchmark()
    {
        var schools = Fixture.Build<School>().CreateMany(30).ToArray();
        
        _benchmarkApi.Reset();
        _benchmarkApi.Setup(api => api.CreateComparatorSet(It.IsAny<PostBenchmarkSetRequest?>())).ReturnsAsync(ApiResult.Ok(new ComparatorSet<School> { TotalResults = schools.Length, Results = schools }));
        _benchmarkApi.Setup(api => api.GetSchoolSizeBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        _benchmarkApi.Setup(api => api.GetFreeSchoolMealBandings(It.IsAny<ApiQuery?>())).ReturnsAsync(ApiResult.Ok(Array.Empty<Banding>()));
        return this;
    }
}