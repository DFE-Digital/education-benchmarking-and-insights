using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightMiscellaneousSteps
{
    private const string FinanceYearKey = "get-finance-year";
    private readonly InsightApiDriver _api;

    public InsightMiscellaneousSteps(InsightApiDriver api)
    {
        _api = api;
    }

    [Given("a valid finance year request")]
    public void GivenAValidFinanceYearRequest()
    {
        _api.CreateRequest(FinanceYearKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/finance-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the finance year request")]
    public async Task WhenISubmitTheFinanceYearRequest()
    {
        await _api.Send();
    }

    [Then("the finance year result should be ok")]
    public async Task ThenTheFinanceYearResultShouldBeOk()
    {
        var response = _api[FinanceYearKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var jsonContent = JsonConvert.DeserializeObject<JObject>(content);

        jsonContent.Should().ContainKey("academies");
        jsonContent.Should().ContainKey("maintainedSchools");
    }
}