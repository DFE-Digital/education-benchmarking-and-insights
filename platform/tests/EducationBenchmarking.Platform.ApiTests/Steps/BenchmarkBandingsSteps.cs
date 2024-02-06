using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using FluentAssertions;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class BenchmarkBandingsSteps
{
    private const string FsmBandingKey = "free-school-meal-banding";
    private const string SchoolSizeBandingKey = "school-size-banding";
    private readonly BenchmarkApiDriver _api;

    public BenchmarkBandingsSteps(BenchmarkApiDriver api)
    {
        _api = api;
    }

    [Given("a valid fsm banding request")]
    public void GivenAValidFsmBandingRequest()
    {
        _api.CreateRequest(FsmBandingKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/free-school-meal/bandings", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the free school meal banding result should be ok")]
    public void ThenTheFreeSchoolMealBandingResultShouldBeOk()
    {
        var response = _api[FsmBandingKey].Response;
        
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [When("I submit the banding request")]
    public async Task WhenISubmitTheBandingRequest()
    {
        await _api.Send();
    }

    [Then("the school size banding result should be ok")]
    public void ThenTheSchoolSizeBandingResultShouldBeOk()
    {
        var response = _api[SchoolSizeBandingKey].Response;
        
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid school size banding request")]
    public void GivenAValidSchoolSizeBandingRequest()
    {
        _api.CreateRequest(SchoolSizeBandingKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/school-size/bandings", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}