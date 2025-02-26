using System.Net;
using FluentAssertions;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Local authorities high needs endpoints")]
public class LocalAuthoritiesHighNeedsSteps(LocalAuthorityFinancesApiDriver api)
{
    private const string HistoryKey = "history";
    private History<LocalAuthorityHighNeedsYear>? _result;

    [Given("a valid high needs request with LA codes '(.*)'")]
    public void GivenAValidHighNeedsRequestWithLaCodes(string codes)
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/high-needs/history?code={string.Join("&code=", codes.Split(","))}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid high needs request")]
    public void GivenAnInvalidHighNeedsRequest()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/high-needs/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the high needs request")]
    public async Task WhenISubmitTheHighNeedsRequest()
    {
        await api.Send();
    }

    [Then("the high needs result should be ok and have the following values:")]
    public async Task ThenTheHighNeedsResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[HistoryKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        _result = content.FromJson<History<LocalAuthorityHighNeedsYear>>();
        var startYear = table.Rows.First()["Start year"];
        var endYear = table.Rows.First()["End year"];

        Assert.Equal(startYear, _result.StartYear.ToString());
        Assert.Equal(endYear, _result.EndYear.ToString());
    }

    [Then("the high needs result should contain the following outturn values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnValuesFor(string year, DataTable table)
    {
        var outturn = OutturnResultForYear(year);
        Assert.NotNull(outturn);
        table.CompareToInstance(outturn);
    }

    [Then("the high needs result should contain the following outturn high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnHighNeedsAmountValuesFor(string year, DataTable table)
    {
        var highNeedsAmount = OutturnResultForYear(year)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs result should contain the following outturn maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnMaintainedValuesFor(string year, DataTable table)
    {
        var maintained = OutturnResultForYear(year)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs result should contain the following outturn non maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnNonMaintainedValuesFor(string year, DataTable table)
    {
        var nonMaintained = OutturnResultForYear(year)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs result should contain the following outturn place funding values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnPlaceFundingValuesFor(string year, DataTable table)
    {
        var placeFunding = OutturnResultForYear(year)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs result should contain the following budget values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetValuesFor(string year, DataTable table)
    {
        var budget = BudgetResultForYear(year);
        Assert.NotNull(budget);
        table.CompareToInstance(budget);
    }

    [Then("the high needs result should contain the following budget high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetHighNeedsAmountValuesFor(string year, DataTable table)
    {
        var highNeedsAmount = BudgetResultForYear(year)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs result should contain the following budget maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetMaintainedValuesFor(string year, DataTable table)
    {
        var maintained = BudgetResultForYear(year)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs result should contain the following budget non maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetNonMaintainedValuesFor(string year, DataTable table)
    {
        var nonMaintained = BudgetResultForYear(year)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs result should contain the following budget place funding values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetPlaceFundingValuesFor(string year, DataTable table)
    {
        var placeFunding = BudgetResultForYear(year)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs result should be bad request")]
    public void ThenTheHighNeedsResultShouldBeBadRequest()
    {
        var response = api[HistoryKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private LocalAuthorityHighNeedsYear? OutturnResultForYear(string year)
    {
        return _result?.Outturn?.SingleOrDefault(o => o.Year.ToString() == year);
    }

    private LocalAuthorityHighNeedsYear? BudgetResultForYear(string year)
    {
        return _result?.Budget?.SingleOrDefault(o => o.Year.ToString() == year);
    }
}