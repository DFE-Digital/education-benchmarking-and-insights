using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightMiscellaneousSteps : InsightSteps
{
    private const string GetFinanceYearKey = "get-finance-year";

    public InsightMiscellaneousSteps(ITestOutputHelper output) : base(output)
    {
    }

    [Given(@"a valid finance year request")]
    public void GivenAValidFinanceYearRequest()
    {
        Api.CreateRequest(GetFinanceYearKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/finance-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When(@"I submit the finance year request")]
    public async Task WhenISubmitTheFinanceYearRequest()
    {
        await Api.Send();
    }

    [Then(@"the finance year result should be ok")]
    public async Task ThenTheFinanceYearResultShouldBeOk()
    {
        var response = Api[GetFinanceYearKey].Response ?? throw new NullException(Api[GetFinanceYearKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        var jsonContent = JsonConvert.DeserializeObject<JObject>(content);
        jsonContent.Should().ContainKey("academies");
        jsonContent.Should().ContainKey("maintainedSchools");
    }
}