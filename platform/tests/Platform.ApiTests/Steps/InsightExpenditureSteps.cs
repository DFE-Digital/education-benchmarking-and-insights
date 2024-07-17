using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightExpenditureSteps(InsightApiDriver api)
{
    private const string SchoolFinancesKey = "get-school-finances";

    [Given("a valid schools expenditure request with urn '(.*)' and '(.*)'")]
    public void GivenAValidSchoolsExpenditureRequestWithUrnAnd(string urn1, string urn2)
    {
        api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn1}%2c{urn2}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithPageAndUrn(string size, string urn)
    {
        api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&page={size}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure request with size '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithSizeAndUrn(string pageSize, string urn)
    {
        api.CreateRequest(SchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights expenditure request")]
    public async Task WhenISubmitTheInsightsExpenditureRequest()
    {
        await api.Send();
    }
}