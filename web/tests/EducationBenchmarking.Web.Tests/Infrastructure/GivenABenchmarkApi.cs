using EducationBenchmarking.Web.Infrastructure.Apis;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Infrastructure;

public class GivenABenchmarkApi : ApiClientTestBase
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new BenchmarkApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }
    
    [Fact]
    public async Task UpsertFinancialPlanShouldCallCorrectUrl()
    {
        var api = new BenchmarkApi(HttpClient);

        await api.UpsertFinancialPlan(new PutFinancialPlanRequest {Urn = "12345", Year = 2023});
            
        VerifyCall(HttpMethod.Put, "api/financial-plan/12345/2023");
    }
    
    [Fact]
    public async Task GetFinancialPlanShouldCallCorrectUrl()
    {
        var api = new BenchmarkApi(HttpClient);

        await api.GetFinancialPlan("12345",2023);
            
        VerifyCall(HttpMethod.Get, "api/financial-plan/12345/2023");
    }
}