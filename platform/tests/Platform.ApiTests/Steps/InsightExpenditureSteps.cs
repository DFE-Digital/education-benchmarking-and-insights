using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Expenditure;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightExpenditureSteps(InsightApiDriver api)
{
    private const string SchoolExpenditureKey = "school-expenditure";
    private const string TrustExpenditureKey = "trust-expenditure";

    [Given("a valid school expenditure dimension request")]
    public void GivenAValidSchoolExpenditureDimensionRequest()
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/dimensions", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure category request")]
    public void GivenAValidSchoolExpenditureCategoryRequest()
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/categories", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure request with urn '(.*)', category '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithUrnCategoryDimensionAndExcludeCentralServices(
        string urn,
        string category,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/expenditure/school/{urn}?category={category}&dimension={dimension}&excludeCentralServices={excludeCentralServices}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school expenditure request with urn '(.*)'")]
    public void GivenAnInvalidSchoolExpenditureRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure history request with urn '(.*)'")]
    public void GivenAValidSchoolExpenditureHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/school/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure query request with urns:")]
    public void GivenAValidSchoolExpenditureQueryRequestWithUrns(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/schools?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure request with company number '(.*)', category '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidTrustExpenditureRequestWithCompanyNumberCategoryDimensionAndExcludeCentralServices(
        string companyNumber,
        string category,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/expenditure/trust/{companyNumber}?category={category}&dimension={dimension}&excludeCentralServices={excludeCentralServices}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid trust expenditure request with company number '(.*)'")]
    public void GivenAnInvalidTrustExpenditureRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/trust/{companyNumber}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure history request with company number '(.*)'")]
    public void GivenAValidTrustExpenditureHistoryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/trust/{companyNumber}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure query request with company numbers:")]
    public void GivenAValidTrustExpenditureQueryRequestWithCompanyNumbers(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/trusts?companyNumbers={string.Join("&companyNumbers=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights expenditure request")]
    public async Task WhenISubmitTheInsightsExpenditureRequest()
    {
        await api.Send();
    }

    [Then("the expenditure dimensions result should be ok and contain:")]
    public async Task ThenTheExpenditureDimensionsResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolExpenditureKey].Response;

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

    [Then("the expenditure categories result should be ok and contain:")]
    public async Task ThenTheExpenditureCategoriesResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolExpenditureKey].Response;

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

    [Then("the school expenditure result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolExpenditureResponse>();
        table.CompareToInstance(result);
    }

    [Then("the school expenditure result should be not found")]
    public void ThenTheSchoolExpenditureResultShouldBeNotFound()
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school expenditure history result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolExpenditureHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the school expenditure query result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolExpenditureResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust expenditure result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustExpenditureResponse>();
        table.CompareToInstance(result);
    }

    [Then("the trust expenditure result should be not found")]
    public void ThenTheTrustExpenditureResultShouldBeNotFound()
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust expenditure history result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustExpenditureHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust expenditure query result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustExpenditureResponse[]>();
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