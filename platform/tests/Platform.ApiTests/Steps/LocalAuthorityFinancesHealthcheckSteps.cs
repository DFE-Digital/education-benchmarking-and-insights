using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Local authority finances healthcheck endpoint")]
public class LocalAuthorityFinancesHealthcheckSteps(LocalAuthorityFinancesApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid local authority finances health check request")]
    private void GivenAValidLocalAuthorityFinancesHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the local authority finances health check request")]
    private async Task WhenISubmitTheLocalAuthorityFinancesHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the local authority finances health check result should be healthy")]
    private async Task ThenTheLocalAuthorityFinancesHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}