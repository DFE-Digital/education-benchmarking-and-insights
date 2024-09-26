using System.Net;
using FluentAssertions;
using Platform.ApiTests.Drivers;
namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Benchmark Comparators Endpoint Testing")]
public class BenchmarkComparatorsSteps(BenchmarkApiDriver api)
{
    private const string ComparatorSchoolsKey = "comparator-schools";
    private const string ComparatorTrustsKey = "comparator-trusts";

    [Given("a valid comparator schools request")]
    private void GivenAValidComparatorSchoolsRequest()
    {
        api.CreateRequest(ComparatorSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/comparators/schools", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [When("I submit the comparator schools request")]
    [When("I submit the comparator trusts request")]
    public async Task WhenISubmitTheComparatorSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the comparator schools should return 410 Gone")]
    private void ThenTheComparatorSchoolsShouldReturnGone()
    {
        var result = api[ComparatorSchoolsKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Gone);
    }

    [Given("a valid comparator trusts request")]
    private void GivenAValidComparatorTrustsRequest()
    {
        api.CreateRequest(ComparatorTrustsKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/comparators/trusts", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Then("the comparator trusts should return 410 Gone")]
    private void ThenTheComparatorTrustsShouldReturnGone()
    {
        var result = api[ComparatorTrustsKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.Gone);
    }
}