using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
public class BudgetForecastBalanceSteps(InsightApiDriver api)
{
    private const string BudgetForecastKey = "budget-forecast";

    [Given("a valid budget forecast request for company number '(.*)' with runType '(.*)', category '(.*)' and runId '(.*)'")]
    public void GivenAValidBudgetForecastRequestForCompanyNumberWithRunTypeCategoryAndRunId(string companyNumber, string runType, string category, string runId)
    {
        api.CreateRequest(BudgetForecastKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/budget-forecast/{companyNumber}?runType={runType}&category={category}&runId={runId}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid budget forecast metrics request for company number '(.*)'")]
    public void GivenAValidBudgetForecastMetricsRequestForCompanyNumber(string companyNumber)
    {
        api.CreateRequest(BudgetForecastKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/budget-forecast/{companyNumber}/metrics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the budget forecast request")]
    public async Task WhenISubmitTheBudgetForecastRequest()
    {
        await api.Send();
    }

    [Then("the budget forecast result should be ok and contain:")]
    public async Task ThenTheBudgetForecastResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[BudgetForecastKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BudgetForecastReturnResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the budget forecast metrics result should be ok and contain:")]
    public async Task ThenTheBudgetForecastMetricsResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[BudgetForecastKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<BudgetForecastReturnMetricResponse[]>();
        table.CompareToSet(result);
    }
}