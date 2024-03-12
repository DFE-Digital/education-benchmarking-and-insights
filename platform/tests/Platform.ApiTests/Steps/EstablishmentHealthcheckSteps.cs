using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentHealthcheckSteps
{
    private const string RequestKey = "health-check";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentHealthcheckSteps(EstablishmentApiDriver api)
    {
        _api = api;
    }

    [Given("a valid establishment health check request")]
    private void GivenAValidEstablishmentHealthCheckRequest()
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the establishment health check request")]
    private async Task WhenISubmitTheEstablishmentHealthCheckRequest()
    {
        await _api.Send();
    }

    [Then("the establishment health check result should be healthy")]
    private async Task ThenTheEstablishmentHealthCheckResultShouldBeHealthy()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await result.Content.ReadAsStringAsync();

        content.Should().Be("Healthy");
    }
}