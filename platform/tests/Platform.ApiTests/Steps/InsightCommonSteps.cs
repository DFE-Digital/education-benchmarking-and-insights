using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Drivers;
using SpecFlow.Internal.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightCommonSteps(InsightApiDriver api)
{
    private const string CommonKey = "common";

    [Given("a current return years request")]
    public void GivenACurrentReturnYearsRequest()
    {
        api.CreateRequest(CommonKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/current-return-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the current return years result should be ok")]
    public async Task ThenTheCurrentReturnYearsResultShouldBeOk()
    {
        var response = api[CommonKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be(new
        {
            aar = "2022",
            cfr = "2022"
        }.ToJson());
    }
}