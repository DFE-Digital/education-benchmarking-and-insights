using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Local authorities high needs endpoints")]
public class LocalAuthoritiesHighNeedsSteps(LocalAuthorityFinancesApiDriver api)
{
    private const string Key = "high-needs";
    private const string HistoryKey = "high-needs-history";
    private History<HighNeedsYear>? _historyResult;
    private LocalAuthority<HighNeeds>[] _results = [];

    [Given("a valid high needs history request with LA codes:")]
    public void GivenAValidHighNeedsHistoryRequestWithLaCodes(DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/high-needs/history?code={string.Join("&code=", codes)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid high needs request with LA codes:")]
    public void GivenAValidHighNeedsRequestWithLaCodes(DataTable table)
    {
        var codes = table.Rows.Select(r => r["Code"]);
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/high-needs?code={string.Join("&code=", codes)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid high needs history request")]
    public void GivenAnInvalidHighNeedsHistoryRequest()
    {
        api.CreateRequest(HistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/high-needs/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid high needs request")]
    public void GivenAnInvalidHighNeedsRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/high-needs", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the high needs request")]
    public async Task WhenISubmitTheHighNeedsRequest()
    {
        await api.Send();
    }

    [Then("the high needs history result should be ok and have the following values:")]
    public async Task ThenTheHighNeedsHistoryResultShouldBeOkAndHaveTheFollowingValues(DataTable table)
    {
        var response = api[HistoryKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        _historyResult = content.FromJson<History<HighNeedsYear>>();
        var startYear = table.Rows.First()["Start year"];
        var endYear = table.Rows.First()["End year"];

        Assert.Equal(startYear, _historyResult.StartYear.ToString());
        Assert.Equal(endYear, _historyResult.EndYear.ToString());
    }

    [Then("the high needs result should be ok and have the following values for '(.*)':")]
    [Then("the high needs result should have the following values for '(.*)':")]
    public async Task ThenTheHighNeedsResultShouldBeOkAndHaveTheFollowingValuesFor(string code, DataTable table)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        _results = content.FromJson<LocalAuthority<HighNeeds>[]>();
        var result = _results.SingleOrDefault(r => r.Code == code);
        Assert.NotNull(result);

        table.CompareToInstance(result);
    }

    [Then("the high needs history result should contain the following outturn values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingOutturnValuesFor(string year, DataTable table)
    {
        var outturn = OutturnResultForYear(year);
        Assert.NotNull(outturn);
        table.CompareToInstance(outturn);
    }

    [Then("the high needs result should contain the following outturn values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnValuesFor(string code, DataTable table)
    {
        var outturn = OutturnResultForCode(code);
        Assert.NotNull(outturn);
        table.CompareToInstance(outturn);
    }

    [Then("the high needs history result should contain the following outturn high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingOutturnHighNeedsAmountValuesFor(string year, DataTable table)
    {
        var highNeedsAmount = OutturnResultForYear(year)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs result should contain the following outturn high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnHighNeedsAmountValuesFor(string code, DataTable table)
    {
        var highNeedsAmount = OutturnResultForCode(code)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs history result should contain the following outturn maintained values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingOutturnMaintainedValuesFor(string year, DataTable table)
    {
        var maintained = OutturnResultForYear(year)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs result should contain the following outturn maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnMaintainedValuesFor(string code, DataTable table)
    {
        var maintained = OutturnResultForCode(code)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs history result should contain the following outturn non maintained values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingOutturnNonMaintainedValuesFor(string year, DataTable table)
    {
        var nonMaintained = OutturnResultForYear(year)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs result should contain the following outturn non maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnNonMaintainedValuesFor(string code, DataTable table)
    {
        var nonMaintained = OutturnResultForCode(code)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs history result should contain the following outturn place funding values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingOutturnPlaceFundingValuesFor(string year, DataTable table)
    {
        var placeFunding = OutturnResultForYear(year)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs result should contain the following outturn place funding values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingOutturnPlaceFundingValuesFor(string code, DataTable table)
    {
        var placeFunding = OutturnResultForCode(code)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs history result should contain the following budget values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingBudgetValuesFor(string year, DataTable table)
    {
        var budget = BudgetResultForYear(year);
        Assert.NotNull(budget);
        table.CompareToInstance(budget);
    }

    [Then("the high needs result should contain the following budget values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetValuesFor(string code, DataTable table)
    {
        var budget = BudgetResultForCode(code);
        Assert.NotNull(budget);
        table.CompareToInstance(budget);
    }

    [Then("the high needs history result should contain the following budget high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingBudgetHighNeedsAmountValuesFor(string year, DataTable table)
    {
        var highNeedsAmount = BudgetResultForYear(year)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs result should contain the following budget high needs amount values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetHighNeedsAmountValuesFor(string code, DataTable table)
    {
        var highNeedsAmount = BudgetResultForCode(code)?.HighNeedsAmount;
        Assert.NotNull(highNeedsAmount);
        table.CompareToInstance(highNeedsAmount);
    }

    [Then("the high needs history result should contain the following budget maintained values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingBudgetMaintainedValuesFor(string year, DataTable table)
    {
        var maintained = BudgetResultForYear(year)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs result should contain the following budget maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetMaintainedValuesFor(string code, DataTable table)
    {
        var maintained = BudgetResultForCode(code)?.Maintained;
        Assert.NotNull(maintained);
        table.CompareToInstance(maintained);
    }

    [Then("the high needs history result should contain the following budget non maintained values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingBudgetNonMaintainedValuesFor(string year, DataTable table)
    {
        var nonMaintained = BudgetResultForYear(year)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs result should contain the following budget non maintained values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetNonMaintainedValuesFor(string code, DataTable table)
    {
        var nonMaintained = BudgetResultForCode(code)?.NonMaintained;
        Assert.NotNull(nonMaintained);
        table.CompareToInstance(nonMaintained);
    }

    [Then("the high needs history result should contain the following budget place funding values for '(.*)':")]
    public void ThenTheHighNeedsHistoryResultShouldContainTheFollowingBudgetPlaceFundingValuesFor(string year, DataTable table)
    {
        var placeFunding = BudgetResultForYear(year)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs result should contain the following budget place funding values for '(.*)':")]
    public void ThenTheHighNeedsResultShouldContainTheFollowingBudgetPlaceFundingValuesFor(string code, DataTable table)
    {
        var placeFunding = BudgetResultForCode(code)?.PlaceFunding;
        Assert.NotNull(placeFunding);
        table.CompareToInstance(placeFunding);
    }

    [Then("the high needs history result should be bad request")]
    public void ThenTheHighNeedsHistoryResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[HistoryKey].Response);
    }

    [Then("the high needs result should be bad request")]
    public void ThenTheHighNeedsResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[Key].Response);
    }

    private HighNeedsYear? OutturnResultForYear(string year)
    {
        return _historyResult?.Outturn?.SingleOrDefault(o => o.Year.ToString() == year);
    }

    private HighNeeds? OutturnResultForCode(string code)
    {
        return _results.SingleOrDefault(r => r.Code == code)?.Outturn;
    }

    private HighNeedsYear? BudgetResultForYear(string year)
    {
        return _historyResult?.Budget?.SingleOrDefault(o => o.Year.ToString() == year);
    }

    private HighNeeds? BudgetResultForCode(string code)
    {
        return _results.SingleOrDefault(r => r.Code == code)?.Budget;
    }
}