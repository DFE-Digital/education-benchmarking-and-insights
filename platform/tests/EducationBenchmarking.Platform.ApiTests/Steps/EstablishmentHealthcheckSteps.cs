using System.Net;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentHealthcheckSteps : EstablishmentSteps
{
    private const string RequestKey = "health-check";

    public EstablishmentHealthcheckSteps(ITestOutputHelper output) : base(output)
    {
    }

    [Given("a valid establishment health check request")]
    private void GivenAValidEstablishmentHealthCheckRequest()
    {
        Api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the establishment health check request")]
    private async Task WhenISubmitTheEstablishmentHealthCheckRequest()
    {
        await Api.Send();
    }

    [Then("the establishment health check result should be healthy")]
    private async Task ThenTheEstablishmentHealthCheckResultShouldBeHealthy()
    {
        var result = Api[RequestKey].Response ?? throw new NullException(Api[RequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();
        content.Should().Be("Healthy");
    }
}