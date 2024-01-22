using System.Net;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class BenchmarkBandingsSteps : BenchmarkSteps
{
    private const string FsmBandingKey = "free-school-meal-banding";
    private const string SchoolSizeBandingKey = "school-size-banding";

    public BenchmarkBandingsSteps(ITestOutputHelper output) : base(output)
    {
    }

    [Given(@"a valid fsm banding request")]
    public void GivenAValidFsmBandingRequest()
    {
        Api.CreateRequest(FsmBandingKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/free-school-meal/bandings", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the free school meal banding result should be ok")]
    public void ThenTheFreeSchoolMealBandingResultShouldBeOk()
    {
        var response = Api[FsmBandingKey].Response ?? throw new NullException(Api[FsmBandingKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [When(@"I submit the banding request")]
    public async Task WhenISubmitTheBandingRequest()
    {
        await Api.Send();
    }

    [Then(@"the school size banding result should be ok")]
    public void ThenTheSchoolSizeBandingResultShouldBeOk()
    {
        var response = Api[SchoolSizeBandingKey].Response ??
                       throw new NullException(Api[SchoolSizeBandingKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given(@"a valid school size banding request")]
    public void GivenAValidSchoolSizeBandingRequest()
    {
        Api.CreateRequest(SchoolSizeBandingKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/school-size/bandings", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}