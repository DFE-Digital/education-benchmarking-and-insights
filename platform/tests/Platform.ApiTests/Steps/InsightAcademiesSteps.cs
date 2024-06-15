using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;
using Platform.Domain;
using Platform.Functions.Extensions;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightAcademiesSteps
{
    private const string AcademyKey = "get-academy";
    private readonly InsightApiDriver _api;

    public InsightAcademiesSteps(InsightApiDriver api)
    {
        _api = api;
    }

    [Given("a valid academy request with urn '(.*)'")]
    public void GivenAValidAcademyRequestWithUrn(string urn)
    {
        _api.CreateRequest(AcademyKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/academy/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the academies request")]
    public async Task WhenISubmitTheAcademiesRequest()
    {
        await _api.Send();
    }

    [Given("a invalid academy request")]
    public void GivenAInvalidAcademyRequest()
    {
        _api.CreateRequest(AcademyKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/academy/00", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the academies result should be not found")]
    public void ThenTheAcademiesResultShouldBeNotFound()
    {
        var response = _api[AcademyKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}