using Platform.Api.Insight.Features.Census.Responses;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightCensusSteps(InsightApiDriver api)
{
    private const string CensusKey = "census";

    [Given("a valid school census request with urn '(.*)'")]
    public void GivenAValidSchoolCensusRequestWithUrn(string urn)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/{urn}", UriKind.Relative),
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
    public void GivenAValidSchoolCensusQueryRequestWithUrns(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census query request with company number '(.*)' and phase '(.*)'")]
    public void GivenAValidSchoolCensusQueryRequestWithCompanyNumberAndPhase(string companyNumber, string phase)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census?companyNumber={companyNumber}&phase={phase}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school census query request with LA code '(.*)' and phase '(.*)'")]
    public void GivenAValidSchoolCensusQueryRequestWithLaCodeAndPhase(string laCode, string phase)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census?laCode={laCode}&phase={phase}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school average across comparator set census history request with urn '(.*)' and dimension '(.*)'")]
    public void GivenASchoolAverageAcrossComparatorSetCensusHistoryRequestWithUrnAndDimension(string urn, string dimension)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/{urn}/history/comparator-set-average?dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school average across comparator set census history request with dimension '(.*)', phase '(.*)', financeType '(.*)'")]
    public void GivenASchoolAverageAcrossComparatorSetCensusHistoryRequestWithDimensionPhaseFinanceType(string dimension, string phase, string financeType)
    {
        api.CreateRequest(CensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/census/history/national-average?dimension={dimension}&phase={phase}&financeType={financeType}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights census request")]
    public async Task WhenISubmitTheInsightsCensusRequest()
    {
        await api.Send();
    }

    [Then("the census result should be ok and contain:")]
    public async Task ThenTheCensusResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[CensusKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusSchoolResponse>();
        table.CompareToInstance(result);
    }

    [Then("the census result should be not found")]
    public void ThenTheCensusResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[CensusKey].Response);
    }

    [Then("the census history result should be ok and contain:")]
    public async Task ThenTheCensusHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[CensusKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the census query result should be ok and contain:")]
    public async Task ThenTheCensusQueryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[CensusKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CensusSchoolResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the census result should be bad request")]
    public void ThenTheCensusResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[CensusKey].Response);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}