using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class BenchmarkHealthcheckSteps : BenchmarkSteps
{
    private const string RequestKey = "health-check";

    public BenchmarkHealthcheckSteps(ITestOutputHelper output) : base(output)
    {
    }
    
    [Given("a valid benchmark health check request")]
    private void GivenAValidBenchmarkHealthCheckRequest()
    {
        Api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
    
    [When("I submit the benchmark health check request")]
    private async Task WhenISubmitTheBenchmarkHealthCheckRequest()
    {
        await Api.Send();
    }

    [Then("the benchmark health check result should be healthy")]
    private async Task ThenTheBenchmarkHealthCheckResultShouldBeHealthy()
    {
        var result = Api[RequestKey].Response ?? throw new NullException(Api[RequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }
}