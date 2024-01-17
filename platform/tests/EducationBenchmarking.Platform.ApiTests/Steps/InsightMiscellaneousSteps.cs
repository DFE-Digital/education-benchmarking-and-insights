using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightMiscellaneousSteps
{
    private const string GetFinanceYearKey = "get-finance-year";
    private readonly ApiDriver _api = new(Config.Apis.Insight ?? throw new NullException(Config.Apis.Insight));

    [Given(@"a valid finance year request")]
    public void GivenAValidFinanceYearRequest()
    {
        _api.CreateRequest(GetFinanceYearKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/finance-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When(@"I submit the finance year request")]
    public async Task WhenISubmitTheFinanceYearRequest()
    {
        await _api.Send();
    }

    [Then(@"the finance year result should be ok")]
    public async Task ThenTheFinanceYearResultShouldBeOk()
    {
        var response = _api[GetFinanceYearKey].Response ?? throw new NullException(_api[GetFinanceYearKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        var jsonContent = JsonConvert.DeserializeObject<JObject>(content);
        jsonContent.Should().ContainKey("academies");
        jsonContent.Should().ContainKey("maintainedSchools");
    }
}