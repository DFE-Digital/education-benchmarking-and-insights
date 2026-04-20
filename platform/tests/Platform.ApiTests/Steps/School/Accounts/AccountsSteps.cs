using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School.Accounts;

[Binding]
[Scope(Feature = "School Accounts")]
public class AccountsSteps(SchoolApiDriver api)
{
    private const string ItSpendingKey = "it-spending";
    private const string IncomeKey = "income";
    private const string IncomeHistoryKey = "income-history";
    private const string BalanceKey = "balance";
    private const string BalanceHistoryKey = "balance-history";
    private const string ExpenditureKey = "expenditure";
    private const string SchoolExpenditureKey = "school-expenditure";
    private const string SchoolExpenditureHistoryKey = "school-expenditure-history";
    private const string ExpenditureSetAverageHistoryKey = "expenditure-set-average-history";
    private const string ExpenditureNationalAverageHistoryKey = "expenditure-national-average-history";
    
    private const string RouteFolder = "School";
    private const string SubFolder = "Accounts";

    [Given("an IT spending request with dimension '(.*)'")]
    public void GivenAnItSpendingRequestWithDimension(string dimension)
    {
        api.CreateRequest(ItSpendingKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/accounts/it-spending?urns=777042&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an IT spending request without dimension")]
    public void GivenAnItSpendingRequestWithoutDimension()
    {
        api.CreateRequest(ItSpendingKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/accounts/it-spending?urns=777042", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an income request with URN '(.*)'")]
    public void GivenAnIncomeRequestWithUrn(string urn)
    {
        api.CreateRequest(IncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/income", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an income history request with URN '(.*)' and dimension '(.*)'")]
    public void GivenAnIncomeHistoryRequestWithUrnAndDimension(string urn, string dimension)
    {
        api.CreateRequest(IncomeHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/income/history?dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a balance request with URN '(.*)'")]
    public void GivenABalanceRequestWithUrn(string urn)
    {
        api.CreateRequest(BalanceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/balance", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a balance history request with URN '(.*)'")]
    public void GivenABalanceHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(BalanceHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/balance/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an expenditure request with query parameters:")]
    public void GivenAnExpenditureRequestWithQueryParameters(DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(ExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/accounts/expenditure{queryString}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school expenditure request with URN '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenASchoolExpenditureRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/expenditure?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school expenditure history request with URN '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenASchoolExpenditureHistoryRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(SchoolExpenditureHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/accounts/expenditure/history?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an expenditure comparator set average history request with URN '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAnExpenditureComparatorSetAverageHistoryRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(ExpenditureSetAverageHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparator-set-average/accounts/expenditure/history?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an expenditure national average history request with query parameters:")]
    public void GivenAnExpenditureNationalAverageHistoryRequestWithQueryParameters(DataTable table)
    {
        var queryString = BuildQueryString(table);

        api.CreateRequest(ExpenditureNationalAverageHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/national-average/accounts/expenditure/history{queryString}", UriKind.Relative),
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
            case "not found":
                AssertHttpResponse.IsNotFound(response);
                break;
            default:
                Assert.Fail($"unexpected result: {result}");
                break;
        }

        var content = await response.Content.ReadAsStringAsync();
        
        // Handle empty response for 'not found' or empty arrays
        if (string.IsNullOrWhiteSpace(content))
        {
            Assert.Empty(content);
            return;
        }

        var actual = content.StartsWith("[") ? (JToken)JArray.Parse(content) : JObject.Parse(content);

        var expected = content.StartsWith("[") 
            ? (JToken)TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder) 
            : TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }

    private static string GetApiKeyFromFriendlyName(string key) => key switch
    {
        "it spending" => ItSpendingKey,
        "income" => IncomeKey,
        "income history" => IncomeHistoryKey,
        "balance" => BalanceKey,
        "balance history" => BalanceHistoryKey,
        "expenditure" => ExpenditureKey,
        "school expenditure" => SchoolExpenditureKey,
        "school expenditure history" => SchoolExpenditureHistoryKey,
        "expenditure comparator set average history" => ExpenditureSetAverageHistoryKey,
        "expenditure national average history" => ExpenditureNationalAverageHistoryKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };

    private static string BuildQueryString(DataTable table)
    {
        const int nameColumn = 0;
        const int valueColumn = 1;

        var parts = table.Rows
            .Where(x => !string.IsNullOrWhiteSpace(x[nameColumn]))
            .Select(x => $"{x[nameColumn]}={x[valueColumn]}")
            .ToArray();
            
        return parts.Length > 0
            ? $"?{string.Join("&", parts)}"
            : string.Empty;
    }
}