using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Census;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightCensusSteps(InsightApiDriver api)
{
    private const string CensusKey = "census";

    [Given("a valid school census dimension request")]
    public void GivenAValidSchoolCensusDimensionRequest()
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/dimensions", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census category request")]
    public void GivenAValidSchoolCensusCategoryRequest()
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/categories", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census request with urn '(.*)', category '(.*)' and dimension '(.*)'")]
    public void GivenAValidSchoolCensusRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/{urn}?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school census request with urn '(.*)'")]
    public void GivenAnInvalidSchoolCensusRequestWithUrn(string urn)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census history request with urn '(.*)'")]
    public void GivenAValidSchoolCensusHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census query request with urns:")]
    public void GivenAValidSchoolCensusQueryRequestWithUrns(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights census request")]
    public async Task WhenISubmitTheInsightsCensusRequest()
    {
        await api.Send();
    }

    [Then("the census dimensions result should be ok and contain:")]
    public async Task ThenTheCensusDimensionsResultShouldBeOkAndContain(Table table)
    {
        var response = api[CensusKey].Response;

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

    [Then("the census categories result should be ok and contain:")]
    public async Task ThenTheCensusCategoriesResultShouldBeOkAndContain(Table table)
    {
        var response = api[CensusKey].Response;

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

    [Then("the census result should be ok and contain:")]
    public async Task ThenTheCensusResultShouldBeOkAndContain(Table table)
    {
        var response = api[CensusKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusResponse>();
        table.CompareToInstance(result);
    }

    [Then("the census result should be not found")]
    public void ThenTheCensusResultShouldBeNotFound()
    {
        var response = api[CensusKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the census history result should be ok and contain:")]
    public async Task ThenTheCensusHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[CensusKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the census query result should be ok and contain:")]
    public async Task ThenTheCensusQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[CensusKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusResponse[]>();
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