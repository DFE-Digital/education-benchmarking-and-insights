using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using FluentAssertions;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightHealthcheckSteps
{
    private const string RequestKey = "health-check";
    private readonly InsightApiDriver _api;

    public InsightHealthcheckSteps(InsightApiDriver api)
    {
        _api = api;
    }

    [Given("a valid insight health check request")]
    private void GivenAValidInsightHealthCheckRequest()
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight health check request")]
    private async Task WhenISubmitTheInsightHealthCheckRequest()
    {
        await _api.Send();
    }

    [Then("the insight health check result should be healthy")]
    private async Task ThenTheInsightHealthCheckResultShouldBeHealthy()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}