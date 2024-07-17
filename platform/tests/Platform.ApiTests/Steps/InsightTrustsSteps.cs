using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Trusts;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightTrustsSteps(InsightApiDriver api)
{
    private const string InsightTrustsKey = "insight-trusts";

    [Given("a valid trust characteristics request with company numbers:")]
    public void GivenAValidTrustCharacteristicsRequestWithUrns(Table table)
    {
        var companyNumbers = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(InsightTrustsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trusts/characteristics?companyNumbers={string.Join("&companyNumbers=", companyNumbers)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insight trusts request")]
    public async Task WhenISubmitTheInsightTrustsRequest()
    {
        await api.Send();
    }

    [Then("the trust characteristics results should be ok and contain:")]
    public async Task ThenTheTrustCharacteristicsResultsShouldBeOkAndContain(Table table)
    {
        var response = api[InsightTrustsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<TrustCharacteristic[]>();
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