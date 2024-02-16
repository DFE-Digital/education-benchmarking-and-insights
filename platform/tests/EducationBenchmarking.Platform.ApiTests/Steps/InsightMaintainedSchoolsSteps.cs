using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightMaintainedSchoolsSteps
{
    private const string MaintainedSchoolKey = "get-maintained-school";
    private readonly InsightApiDriver _api;

    public InsightMaintainedSchoolsSteps(InsightApiDriver api)
    {
        _api = api;
    }

    [When("I submit the maintained school request")]
    public async Task WhenISubmitTheMaintainedSchoolRequest()
    {
        await _api.Send();
    }

    [Given("a valid maintained school request with urn '(.*)'")]
    public void GivenAValidMaintainedSchoolRequestWithUrn(string urn)
    {
        _api.CreateRequest(MaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/maintained-school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the maintained school result should be ok")]
    public async Task ThenTheMaintainedSchoolResultShouldBeOk()
    {
        var response = _api[MaintainedSchoolKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Finances>();

        result.SchoolName.Should().Be("Stockingford Maintained Nursery School");
        result.Urn.Should().Be("125491");
    }

    [Given("a invalid maintained school request")]
    public void GivenAInvalidMaintainedSchoolRequest()
    {
        _api.CreateRequest(MaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/maintained-school/00", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the maintained school result should be not found")]
    public void ThenTheMaintainedSchoolResultShouldBeNotFound()
    {
        var response = _api[MaintainedSchoolKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}