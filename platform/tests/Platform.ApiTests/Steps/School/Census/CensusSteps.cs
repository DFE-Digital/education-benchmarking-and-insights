using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;

namespace Platform.ApiTests.Steps.School.Census;

[Binding]
[Scope(Feature = "School Census")]
public class CensusSteps(SchoolApiDriver api)
{
    private const string SeniorLeadershipKey = "senior-leadership";
    private const string SchoolCensusKey = "school-census";
    private const string SchoolCensusHistoryKey = "school-census-history";
    private const string ComparatorSetAverageHistoryKey = "comparator-set-average-history";
    private const string NationalAverageHistoryKey = "national-average-history";
    private const string CensusCollectionKey = "census-collection";

    private const string RouteFolder = "School";
    private const string SubFolder = "Census";

    [Given("a valid senior leadership request with URNs '(.*)' and dimension '(.*)'")]
    public void GivenAValidSeniorLeadershipRequestWithUrnsAndDimension(string urns, string dimension)
    {
        api.CreateRequest(SeniorLeadershipKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census/senior-leadership?urns={urns}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid senior leadership request with URNs '(.*)' and no dimension")]
    public void GivenAValidSeniorLeadershipRequestWithUrnsAndNoDimension(string urns)
    {
        api.CreateRequest(SeniorLeadershipKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census/senior-leadership?urns={urns}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid senior leadership request with (.*)")]
    public void GivenAnInvalidSeniorLeadershipRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/census/senior-leadership?urns=123456", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "missing URNs":
                request.RequestUri = new Uri("/api/schools/census/senior-leadership", UriKind.Relative);
                break;
            case "unrecognized dimension":
                request.RequestUri = new Uri("/api/schools/census/senior-leadership?urns=123456&dimension=invalid", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(SeniorLeadershipKey, request);
    }

    [Given("a valid school census request with URN '(.*)'")]
    public void GivenAValidSchoolCensusRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolCensusKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/census", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school census request with (.*)")]
    public void GivenAnInvalidSchoolCensusRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/123456/census", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "non-existent URN":
                request.RequestUri = new Uri("/api/schools/999999/census", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(SchoolCensusKey, request);
    }

    [Given("a valid school census history request with URN '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAValidSchoolCensusHistoryRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(SchoolCensusHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/census/history?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school census history request with (.*)")]
    public void GivenAnInvalidSchoolCensusHistoryRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/123456/census/history", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "unrecognized category":
                request.RequestUri = new Uri("/api/schools/123456/census/history?category=invalid", UriKind.Relative);
                break;
            case "unrecognized dimension":
                request.RequestUri = new Uri("/api/schools/123456/census/history?dimension=invalid", UriKind.Relative);
                break;
            case "non-existent URN":
                request.RequestUri = new Uri("/api/schools/999999/census/history", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(SchoolCensusHistoryKey, request);
    }

    [Given("a valid comparator set average census history request with URN '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAValidComparatorSetAverageCensusHistoryRequestWithUrnCategoryAndDimension(string urn, string category, string dimension)
    {
        api.CreateRequest(ComparatorSetAverageHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparator-set-average/census/history?category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid comparator set average census history request with (.*)")]
    public void GivenAnInvalidComparatorSetAverageCensusHistoryRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/123456/comparator-set-average/census/history", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "unrecognized category":
                request.RequestUri = new Uri("/api/schools/123456/comparator-set-average/census/history?category=invalid", UriKind.Relative);
                break;
            case "unrecognized dimension":
                request.RequestUri = new Uri("/api/schools/123456/comparator-set-average/census/history?dimension=invalid", UriKind.Relative);
                break;
            case "non-existent URN":
                request.RequestUri = new Uri("/api/schools/999999/comparator-set-average/census/history", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(ComparatorSetAverageHistoryKey, request);
    }

    [Given("a valid national average census history request with dimension '(.*)', phase '(.*)', and financeType '(.*)'")]
    public void GivenAValidNationalAverageCensusHistoryRequestWithDimensionPhaseAndFinanceType(string dimension, string phase, string financeType)
    {
        api.CreateRequest(NationalAverageHistoryKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/national-average/census/history?dimension={dimension}&phase={phase}&financeType={financeType}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid national average census history request with criteria yielding no data")]
    public void GivenAValidNationalAverageCensusHistoryRequestWithCriteriaYieldingNoData()
    {
        api.CreateRequest(NationalAverageHistoryKey, new HttpRequestMessage
        {
            // Assuming this specific combination yields no data in the test environment
            RequestUri = new Uri("/api/schools/national-average/census/history?dimension=Total&phase=Nursery&financeType=Academy", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid national average census history request with (.*)")]
    public void GivenAnInvalidNationalAverageCensusHistoryRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/national-average/census/history?dimension=Total&phase=Primary&financeType=Maintained", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "unrecognized dimension":
                request.RequestUri = new Uri("/api/schools/national-average/census/history?dimension=invalid&phase=Primary&financeType=Maintained", UriKind.Relative);
                break;
            case "unrecognized phase":
                request.RequestUri = new Uri("/api/schools/national-average/census/history?dimension=Total&phase=invalid&financeType=Maintained", UriKind.Relative);
                break;
            case "unrecognized financeType":
                request.RequestUri = new Uri("/api/schools/national-average/census/history?dimension=Total&phase=Primary&financeType=invalid", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(NationalAverageHistoryKey, request);
    }

    [Given("a valid census collection request with URNs '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAValidCensusCollectionRequestWithUrnsCategoryAndDimension(string urns, string category, string dimension)
    {
        api.CreateRequest(CensusCollectionKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census?urns={urns}&category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid census collection request with CompanyNumber '(.*)', phase '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAValidCensusCollectionRequestWithCompanyNumberPhaseCategoryAndDimension(string companyNumber, string phase, string category, string dimension)
    {
        api.CreateRequest(CensusCollectionKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census?companyNumber={companyNumber}&phase={phase}&category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid census collection request with LaCode '(.*)', phase '(.*)', category '(.*)', and dimension '(.*)'")]
    public void GivenAValidCensusCollectionRequestWithLaCodePhaseCategoryAndDimension(string laCode, string phase, string category, string dimension)
    {
        api.CreateRequest(CensusCollectionKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/census?laCode={laCode}&phase={phase}&category={category}&dimension={dimension}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid census collection request yielding no results")]
    public void GivenAValidCensusCollectionRequestYieldingNoResults()
    {
        api.CreateRequest(CensusCollectionKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/census?urns=999999", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid census collection request with (.*)")]
    public void GivenAnInvalidCensusCollectionRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/census?urns=123456", UriKind.Relative),
            Method = HttpMethod.Get
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            case "missing URNs, CompanyNumber, and LaCode":
                request.RequestUri = new Uri("/api/schools/census", UriKind.Relative);
                break;
            case "both CompanyNumber and LaCode":
                request.RequestUri = new Uri("/api/schools/census?companyNumber=12345678&laCode=123&phase=Primary", UriKind.Relative);
                break;
            case "missing phase":
                request.RequestUri = new Uri("/api/schools/census?companyNumber=12345678", UriKind.Relative);
                break;
            case "unrecognized category":
                request.RequestUri = new Uri("/api/schools/census?urns=123456&category=invalid", UriKind.Relative);
                break;
            case "unrecognized dimension":
                request.RequestUri = new Uri("/api/schools/census?urns=123456&dimension=invalid", UriKind.Relative);
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(CensusCollectionKey, request);
    }

    [When("I submit the school census request")]
    public async Task WhenISubmitTheSchoolCensusRequest()
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
        "senior leadership" => SeniorLeadershipKey,
        "school census" => SchoolCensusKey,
        "school census history" => SchoolCensusHistoryKey,
        "comparator set average history" => ComparatorSetAverageHistoryKey,
        "national average history" => NationalAverageHistoryKey,
        "census collection" => CensusCollectionKey,
        _ => throw new ArgumentOutOfRangeException(nameof(key), $"Unknown key {key}")
    };
}
