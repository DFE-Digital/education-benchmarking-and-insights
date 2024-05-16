using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Infrastructure;

public class GivenAnInsightApi : ApiClientTestBase
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new InsightApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task GetSchoolFinancesShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetSchoolFinances("123213");

        VerifyCall(HttpMethod.Get, "api/school/123213");
    }

    [Fact]
    public async Task GetSchoolBalanceHistoryShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetSchoolBalanceHistory("123213");

        VerifyCall(HttpMethod.Get, "api/school/123213/balance/history");
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
    public async Task GetFinanceYearsShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetCurrentReturnYears();

        VerifyCall(HttpMethod.Get, "api/current-return-years");
    }

    [Fact]
    public async Task GetSchoolExpenditureShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetSchoolExpenditure("123213");

        VerifyCall(HttpMethod.Get, "api/school/123213/expenditure");
    }

    [Fact]
    public async Task GetSchoolFloorAreaMetricShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetSchoolFloorAreaMetric("123213");

        VerifyCall(HttpMethod.Get, "api/metric/123213/floor-area");
    }
}