using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Schools;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightSchoolsSteps(InsightApiDriver api)
{
    private const string InsightSchoolsKey = "insight-schools";

    [Given("a valid school characteristics request with urn '(.*)'")]
    public void GivenAValidSchoolCharacteristicsRequestWithUrn(string urn)
    {
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{urn}/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school characteristics request with urn '(.*)'")]
    public void GivenAnInvalidSchoolCharacteristicsRequestWithUrn(string urn)
    {
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{urn}/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school characteristics request with urns:")]
    public void GivenAValidSchoolCharacteristicsRequestWithUrns(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(InsightSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/characteristics?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight schools request")]
    public async Task WhenISubmitTheInsightSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the school characteristics result should be ok and contain:")]
    public async Task ThenTheSchoolCharacteristicsResultShouldBeOkAndContain(Table table)
    {
        var response = api[InsightSchoolsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolCharacteristic>();
        table.CompareToInstance(result);
    }

    [Then("the school characteristics result should be not found")]
    public void ThenTheSchoolCharacteristicsResultShouldBeNotFound()
    {
        var response = api[InsightSchoolsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school characteristics results should be ok and contain:")]
    public async Task ThenTheSchoolCharacteristicsResultsShouldBeOkAndContain(Table table)
    {
        var response = api[InsightSchoolsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SchoolCharacteristic[]>();
        table.CompareToSet(results);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(Table table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}