using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Features.Balance;
using Platform.ApiTests.Assist;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightBalanceSteps(InsightApiDriver api)
{
    private const string SchoolBalanceKey = "school-balance";
    private const string TrustBalanceKey = "trust-balance";

    [Given("a valid school balance request with urn '(.*)', dimension '(.*)'")]
    public void GivenAValidSchoolBalanceRequestWithUrnDimension(
        string urn,
        string dimension)
    {
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/balance/school/{urn}?dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school balance request with urn '(.*)'")]
    public void GivenAnInvalidSchoolBalanceRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school balance history request with urn '(.*)'")]
    public void GivenAValidSchoolBalanceHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/school/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust balance request with company number '(.*)', dimension '(.*)'")]
    public void GivenAValidTrustBalanceRequestWithCompanyNumberDimension(
        string companyNumber,
        string dimension)
    {
        api.CreateRequest(TrustBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/balance/trust/{companyNumber}?dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid trust balance request with company number '(.*)'")]
    public void GivenAnInvalidTrustBalanceRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/trust/{companyNumber}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust balance history request with company number '(.*)'")]
    public void GivenAValidTrustBalanceHistoryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/trust/{companyNumber}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust balance query request with company numbers:")]
    public void GivenAValidTrustBalanceQueryRequestWithCompanyNumbers(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(TrustBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/trusts?companyNumbers={string.Join("&companyNumbers=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights balance request")]
    public async Task WhenISubmitTheInsightsBalanceRequest()
    {
        await api.Send();
    }

    [Then("the school balance result should be ok and contain:")]
    public async Task ThenTheSchoolBalanceResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BalanceSchoolResponse>();
        table.CompareToInstance(result);
    }

    [Then("the school balance result should be not found")]
    public void ThenTheSchoolBalanceResultShouldBeNotFound()
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school balance history result should be ok and contain:")]
    public async Task ThenTheSchoolBalanceHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BalanceHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the trust balance result should be ok and contain:")]
    public async Task ThenTheTrustBalanceResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BalanceTrustResponse>();
        table.CompareToInstance(result);
    }

    [Then("the trust balance result should be not found")]
    public void ThenTheTrustBalanceResultShouldBeNotFound()
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust balance history result should be ok and contain:")]
    public async Task ThenTheTrustBalanceHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BalanceHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the trust balance query result should be ok and contain:")]
    public async Task ThenTheTrustBalanceQueryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BalanceTrustResponse[]>();
        table.CompareToSet(result);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}