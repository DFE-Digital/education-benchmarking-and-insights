using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Benchmark healthcheck endpoint")]
public class BenchmarkHealthcheckSteps(BenchmarkApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid benchmark health check request")]
    private void GivenAValidBenchmarkHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the benchmark health check request")]
    private async Task WhenISubmitTheBenchmarkHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the benchmark health check result should be healthy")]
    private async Task ThenTheBenchmarkHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}