using System.Net;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightHealthcheckSteps : InsightSteps
{
    private const string RequestKey = "health-check";

    public InsightHealthcheckSteps(ITestOutputHelper output) : base(output)
    {
    }

    [Given("a valid insight health check request")]
    private void GivenAValidInsightHealthCheckRequest()
    {
        Api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight health check request")]
    private async Task WhenISubmitTheInsightHealthCheckRequest()
    {
        await Api.Send();
    }

    [Then("the insight health check result should be healthy")]
    private async Task ThenTheInsightHealthCheckResultShouldBeHealthy()
    {
        var result = Api[RequestKey].Response ?? throw new NullException(Api[RequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }
}