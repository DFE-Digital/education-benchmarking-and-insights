using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding]
public class BenchmarkHealthcheckSteps
{
    private const string RequestKey = "health-check";
    private readonly BenchmarkApiDriver _api;

    public BenchmarkHealthcheckSteps(BenchmarkApiDriver api)
    {
        _api = api;
    }

    [Given("a valid benchmark health check request")]
    private void GivenAValidBenchmarkHealthCheckRequest()
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the benchmark health check request")]
    private async Task WhenISubmitTheBenchmarkHealthCheckRequest()
    {
        await _api.Send();
    }

    [Then("the benchmark health check result should be healthy")]
    private async Task ThenTheBenchmarkHealthCheckResultShouldBeHealthy()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}