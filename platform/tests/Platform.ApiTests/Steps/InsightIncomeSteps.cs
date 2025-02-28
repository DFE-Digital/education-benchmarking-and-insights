using Platform.Api.Insight.Features.Income.Responses;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Insights income endpoints")]
public class InsightIncomeSteps(InsightApiDriver api)
{
    private const string SchoolIncomeKey = "school-income";
    private const string TrustIncomeKey = "trust-income";

    [Given("a valid school income request with urn '(.*)'")]
    public void GivenAValidSchoolIncomeRequestWithUrn(
        string urn)
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/income/school/{urn}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school income history request with urn '(.*)'")]
    public void GivenAValidSchoolIncomeHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/school/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust income history request with company number '(.*)'")]
    public void GivenAValidTrustIncomeHistoryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/trust/{companyNumber}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights income request")]
    public async Task WhenISubmitTheInsightsIncomeRequest()
    {
        await api.Send();
    }

    [Then("the school income result should be ok and contain:")]
    public async Task ThenTheSchoolIncomeResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolIncomeKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<IncomeSchoolResponse>();
        table.CompareToInstance(result);
    }

    [Then("the school income result should be not found")]
    public void ThenTheSchoolIncomeResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[SchoolIncomeKey].Response);
    }

    [Then("the school income history result should be ok and contain:")]
    public async Task ThenTheSchoolIncomeHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolIncomeKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<IncomeHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the trust income history result should be ok and contain:")]
    public async Task ThenTheTrustIncomeHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustIncomeKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<IncomeHistoryResponse>();
        table.CompareToSet(result.Rows);
    }
}