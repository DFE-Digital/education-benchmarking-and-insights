using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class BenchmarkHealthcheckSteps
{
    private const string RequestKey = "health-check";
    private readonly ApiDriver _api;

    public BenchmarkHealthcheckSteps(ITestOutputHelper output)
    {
        _api = new ApiDriver(Config.Apis.Benchmark ?? throw new NullException(Config.Apis.Benchmark), output);
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
        var result = _api[RequestKey].Response ?? throw new NullException(_api[RequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }
}