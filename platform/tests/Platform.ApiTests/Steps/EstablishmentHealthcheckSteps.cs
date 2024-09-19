using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding, Scope(Feature = "Establishment healthcheck endpoint")]
public class EstablishmentHealthcheckSteps(EstablishmentApiDriver api)
{
    private const string RequestKey = "health-check";

    [Given("a valid establishment health check request")]
    private void GivenAValidEstablishmentHealthCheckRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the establishment health check request")]
    private async Task WhenISubmitTheEstablishmentHealthCheckRequest()
    {
        await api.Send();
    }

    [Then("the establishment health check result should be healthy")]
    private async Task ThenTheEstablishmentHealthCheckResultShouldBeHealthy()
    {
        var result = api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}