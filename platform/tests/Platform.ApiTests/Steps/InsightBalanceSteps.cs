using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Balance;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightBalanceSteps(InsightApiDriver api)
{
    private const string SchoolBalanceKey = "school-balance";
    private const string TrustBalanceKey = "trust-balance";

    [Given("a valid school balance dimension request")]
    public void GivenAValidSchoolBalanceDimensionRequest()
    {
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/dimensions", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school balance request with urn '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidSchoolBalanceRequestWithUrnDimensionAndExcludeCentralServices(
        string urn,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/balance/school/{urn}?dimension={dimension}&excludeCentralServices={excludeCentralServices}",
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

    [Given("a valid school balance query request with urns:")]
    public void GivenAValidSchoolBalanceQueryRequestWithUrns(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(SchoolBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/balance/schools?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust balance request with company number '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidTrustBalanceRequestWithCompanyNumberDimensionAndExcludeCentralServices(
        string companyNumber,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(TrustBalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/balance/trust/{companyNumber}?dimension={dimension}&excludeCentralServices={excludeCentralServices}",
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
    public void GivenAValidTrustBalanceQueryRequestWithCompanyNumbers(Table table)
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

    [Then("the balance dimensions result should be ok and contain:")]
    public async Task ThenTheBalanceDimensionsResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { Dimension = result });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the balance categories result should be ok and contain:")]
    public async Task ThenTheBalanceCategoriesResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { Category = result });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the school balance result should be ok and contain:")]
    public async Task ThenTheSchoolBalanceResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolBalanceResponse>();
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
    public async Task ThenTheSchoolBalanceHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolBalanceHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the school balance query result should be ok and contain:")]
    public async Task ThenTheSchoolBalanceQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolBalanceResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust balance result should be ok and contain:")]
    public async Task ThenTheTrustBalanceResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustBalanceResponse>();
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
    public async Task ThenTheTrustBalanceHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustBalanceHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust balance query result should be ok and contain:")]
    public async Task ThenTheTrustBalanceQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustBalanceKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustBalanceResponse[]>();
        table.CompareToSet(result);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(Table table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}