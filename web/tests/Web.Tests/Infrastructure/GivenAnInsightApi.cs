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
    public async Task GetMaintainedSchoolFinancesShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetMaintainedSchoolFinances("123213");

        VerifyCall(HttpMethod.Get, "api/maintained-school/123213");
    }

    [Fact]
    public async Task GetMaintainedSchoolWorkforceHistoryShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetMaintainedSchoolWorkforceHistory("123213");

        VerifyCall(HttpMethod.Get, "api/maintained-school/123213/workforce/history");
    }

    [Fact]
    public async Task GetMaintainedSchoolBalanceHistoryShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetMaintainedSchoolBalanceHistory("123213");

        VerifyCall(HttpMethod.Get, "api/maintained-school/123213/balance/history");
    }

    [Fact]
    public async Task GetAcademyFinancesShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetAcademyFinances("123213");

        VerifyCall(HttpMethod.Get, "api/academy/123213");
    }

    [Fact]
    public async Task GetAcademyWorkforceHistoryShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetAcademyWorkforceHistory("123213");

        VerifyCall(HttpMethod.Get, "api/academy/123213/workforce/history");
    }

    [Fact]
    public async Task GetAcademyBalanceHistoryShouldCallCorrectUrl()
    {
        var api = new InsightApi(HttpClient);

        await api.GetAcademyBalanceHistory("123213");

        VerifyCall(HttpMethod.Get, "api/academy/123213/balance/history");
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

        await api.GetCurrentReturnYears();

        VerifyCall(HttpMethod.Get, "api/current-return-years");
    }
}