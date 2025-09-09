using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Non financial healthcheck endpoint")]
public class NonFinancialHealthcheckSteps(NonFinancialApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid non financial health check request")]
    private void GivenAValidNonFinancialHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the non financial health check request")]
    private async Task WhenISubmitTheNonFinancialHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the non financial health check result should be healthy")]
    private async Task ThenTheNonFinancialHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}