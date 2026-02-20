using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School;

[Binding]
[Scope(Feature = "School Accounts History Trends")]
public class AccountsHistoryTrendsSteps(SchoolApiDriver api)
{
    private const string IncomeSetKey = "income-set-average";
    private const string IncomeNationalKey = "income-national-average";
    private const string BalanceSetKey = "balance-set-average";
    private const string BalanceNationalKey = "balance-national-average";
    private const string RouteFolder = "School";
    private const string SubFolder = "Accounts";

    [Given("a balance national average history request with query parameters:")]
    public void GivenABalanceNationalAverageHistoryRequestWithQueryParameters(DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(BalanceNationalKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/national-average/accounts/balance/history{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an income national average history request with query parameters:")]
    public void GivenAnIncomeNationalAverageHistoryRequestWithQueryParameters(DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(IncomeNationalKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/national-average/accounts/income/history{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a balance comparator set average history request with URN '(.*)' and dimension '(.*)'")]
    public void GivenABalanceComparatorSetAverageHistoryRequestWithUrnAndDimension(string urn, string dimension)
    {
        api.CreateRequest(BalanceSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparator-set-average/accounts/balance/history?dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an income comparator set average history request with URN '(.*)' and dimension '(.*)'")]
    public void GivenAnIncomeComparatorSetAverageHistoryRequestWithUrnAndDimension(string urn, string dimension)
    {
        api.CreateRequest(IncomeSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparator-set-average/accounts/income/history?dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the '(.*)' result should be '(.*)' and match the expected output in '(.*)'")]
    public async Task ThenTheResultShouldBeAndMatchTheExpectedOutputIn(string key, string result, string testFile)
    {
        var apiKey = GetApiKeyFromFriendlyName(key);

        var response = api[apiKey].Response;

        switch (result)
        {
            case "ok":
                AssertHttpResponse.IsOk(response);
                break;
            case "bad request":
                AssertHttpResponse.IsBadRequest(response);
                break;
            default:
                Assert.Fail($"unexpected result: {result}");
                break;
        }

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    private static string GetApiKeyFromFriendlyName(string key) => key switch
    {
        "income comparator set average history" => IncomeSetKey,
        "income national average history" => IncomeNationalKey,
        "balance comparator set average history" => BalanceSetKey,
        "balance national average history" => BalanceNationalKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };

    private static string BuildQueryString(Table table)
    {
        const int nameColumn = 0;
        const int valueColumn = 1;

        var parts = table.Rows.Select(x => $"{x[nameColumn]}={x[valueColumn]}").ToArray();
        return parts.Length > 0
            ? $"?{string.Join("&", parts)}"
            : string.Empty;
    }
}