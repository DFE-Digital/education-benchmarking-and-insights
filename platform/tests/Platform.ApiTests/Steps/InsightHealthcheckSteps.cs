using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Insight healthcheck endpoint")]
public class InsightHealthcheckSteps(InsightApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid insight health check request")]
    private void GivenAValidInsightHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight health check request")]
    private async Task WhenISubmitTheInsightHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the insight health check result should be healthy")]
    private async Task ThenTheInsightHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}