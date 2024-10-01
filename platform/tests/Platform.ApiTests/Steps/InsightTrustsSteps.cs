﻿using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Trusts;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
namespace Platform.ApiTests.Steps;

[Binding]
public class InsightTrustsSteps(InsightApiDriver api)
{
    private const string InsightTrustsKey = "insight-trusts";

    [Given("a valid trust characteristics request with company numbers:")]
    public void GivenAValidTrustCharacteristicsRequestWithUrns(DataTable table)
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
    public async Task ThenTheTrustCharacteristicsResultsShouldBeOkAndContain(DataTable table)
    {
        var response = api[InsightTrustsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<TrustCharacteristic[]>();
        table.CompareToSet(results);
    }

    [Then("the phases should contain:")]
    public async Task ThenThePhasesShouldContain(DataTable table)
    {
        var response = api[InsightTrustsKey].Response;
        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<TrustCharacteristic[]>();
        table.CompareToSet(results.Select(r => new
        {
            r.CompanyNumber,
            Phases = string.Join(", ", r.Phases.Select(p => $"{p.Phase}: {(p.Count.HasValue ? p.Count.ToString() : "?")}"))
        }));
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private record TrustCharacteristic : Api.Insight.Trusts.TrustCharacteristic
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public new TrustPhase[] Phases { get; set; } = [];
    }
}