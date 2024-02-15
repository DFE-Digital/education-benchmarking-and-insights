using EducationBenchmarking.Web.Infrastructure.Apis;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Infrastructure;

public class GivenAnInsightApi : ApiClientTestBase
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new InsightApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }
    
    [Fact]
    public async Task GetMaintainedSchoolFinancesShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetMaintainedSchoolFinances("123213");
            
        VerifyCall(HttpMethod.Get, "api/maintained-school/123213");
    }
    
    [Fact]
    public async Task GetAcademyFinancesShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetAcademyFinances("123213");
            
        VerifyCall(HttpMethod.Get, "api/academy/123213");
    }
    
    [Fact]
    public async Task GetAcademyFinancesWithQueryShouldCallCorrectUrl()
    {
        var query = new ApiQuery().AddIfNotNull("Name", "Foo");
        var api = new InsightApi(HttpClient);

        await api.GetAcademyFinances("123213", query);
            
        VerifyCall(HttpMethod.Get, "api/academy/123213?Name=Foo");
    }
    
    [Fact]
    public async Task GetSchoolsExpenditureShouldCallCorrectUrl()
    {
        var query = new ApiQuery().AddIfNotNull("Name", "Foo");
        var api = new InsightApi(HttpClient);

        await api.GetSchoolsExpenditure(query);
            
        VerifyCall(HttpMethod.Get, "api/schools/expenditure?Name=Foo");
    }
    
    [Fact]
    public async Task GetSchoolsWorkforceShouldCallCorrectUrl()
    {
        var query = new ApiQuery().AddIfNotNull("Name", "Foo");
        var api = new InsightApi(HttpClient);

        await api.GetSchoolsWorkforce(query);
            
        VerifyCall(HttpMethod.Get, "api/schools/workforce?Name=Foo");
    }
    
    [Fact]
    public async Task GetFinanceYearsShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetFinanceYears();
            
        VerifyCall(HttpMethod.Get, "api/finance-years");
    }
}