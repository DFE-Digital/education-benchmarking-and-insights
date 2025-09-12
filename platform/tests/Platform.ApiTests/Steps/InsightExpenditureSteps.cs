using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Insights expenditure endpoints")]
public class InsightExpenditureSteps(InsightApiDriver api)
{
    private const string SchoolExpenditureKey = "school-expenditure";
    private const string TrustExpenditureKey = "trust-expenditure";

    [Given("a valid school expenditure dimension request")]
    public void GivenAValidSchoolExpenditureDimensionRequest()
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/expenditure/dimensions", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure category request")]
    public void GivenAValidSchoolExpenditureCategoryRequest()
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/expenditure/categories", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(
        "a school expenditure request with urn '(.*)', category '(.*)' and dimension '(.*)'")]
    public void GivenASchoolExpenditureRequestWithUrnCategoryAndDimension(
        string urn,
        string category,
        string dimension)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/expenditure/school/{urn}?category={category}&dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure history request with urn '(.*)'")]
    public void GivenAValidSchoolExpenditureHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/school/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure query request with urns:")]
    public void GivenAValidSchoolExpenditureQueryRequestWithUrns(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/schools?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure query request with company number '(.*)' and phase '(.*)'")]
    public void GivenAValidSchoolExpenditureQueryRequestWithCompanyNumberAndPhase(string companyNumber, string phase)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/schools?companyNumber={companyNumber}&phase={phase}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school expenditure query request with LA code '(.*)' and phase '(.*)'")]
    public void GivenAValidSchoolExpenditureQueryRequestWithLaCodeAndPhase(string laCode, string phase)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/schools?laCode={laCode}&phase={phase}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(
        "a trust expenditure request with company number '(.*)', category '(.*)' and dimension '(.*)'")]
    public void GivenATrustExpenditureRequestWithCompanyNumberCategoryAndDimension(
        string companyNumber,
        string category,
        string dimension)
    {
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/expenditure/trust/{companyNumber}?category={category}&dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure history request with company number '(.*)'")]
    public void GivenAValidTrustExpenditureHistoryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/trust/{companyNumber}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure query request with company numbers:")]
    public void GivenAValidTrustExpenditureQueryRequestWithCompanyNumbers(DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/trusts?companyNumbers={string.Join("&companyNumbers=", urns)}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust expenditure query request for category '(.*)', dimension '(.*)', excludeCentralServices '(.*)', with company numbers:")]
    public void GivenAValidTrustExpenditureQueryRequestWithCompanyNumbersAndOtherParams(
        string category,
        string dimension,
        string excludeCentralServices,
        DataTable table
    )
    {
        var companyNumbers = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(TrustExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/expenditure/trusts?companyNumbers={string.Join(",", companyNumbers)}&category={category}&dimension={dimension}&excludeCentralServices={excludeCentralServices}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a school average across comparator set expenditure history request with urn '(.*)' and dimension '(.*)'")]
    public void GivenASchoolAverageAcrossComparatorSetExpenditureHistoryRequestWithUrnAndDimension(string urn, string dimension)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/expenditure/school/{urn}/history/comparator-set-average?dimension={dimension}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(
        "a school national average expenditure history request with dimension '(.*)', phase '(.*)', financeType '(.*)'")]
    public void GivenASchoolNationalAverageExpenditureHistoryRequestWithDimensionPhaseFinanceType(string dimension, string phase, string financeType)
    {
        api.CreateRequest(SchoolExpenditureKey, new HttpRequestMessage
        {
            RequestUri =
                new Uri(
                    $"/api/expenditure/school/history/national-average?dimension={dimension}&phase={phase}&financeType={financeType}",
                    UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights expenditure request")]
    public async Task WhenISubmitTheInsightsExpenditureRequest()
    {
        await api.Send();
    }

    [Then("the school expenditure result should be not found")]
    public void ThenTheSchoolExpenditureResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[SchoolExpenditureKey].Response);
    }

    [Then("the school expenditure result should be bad request")]
    public void ThenTheSchoolExpenditureResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[SchoolExpenditureKey].Response);
    }

    [Then("the trust expenditure result should be not found")]
    public void ThenTheTrustExpenditureResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[TrustExpenditureKey].Response);
    }

    [Then("the trust expenditure result should be bad request")]
    public void ThenTheTrustExpenditureResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[TrustExpenditureKey].Response);
    }

    [Then("the trust expenditure response should be ok, contain a JSON array and match the expected output of '(.*)'")]
    public async Task ThenTheTrustResponseShouldBeOkAnArrayAndMatchTheExpected(string testFile)
    {
        var response = api[TrustExpenditureKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the trust expenditure response should be ok, contain a JSON object and match the expected output of '(.*)'")]
    public async Task ThenTheTrustResponseShouldBeOkAnObjectAndMatchTheExpected(string testFile)
    {
        var response = api[TrustExpenditureKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the school expenditure response should be ok, contain a JSON array and match the expected output of '(.*)'")]
    public async Task ThenTheSchoolResponseShouldBeOkAnArrayAndMatchTheExpected(string testFile)
    {
        var response = api[SchoolExpenditureKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JArray.Parse(content);

        var expected = TestDataProvider.GetJsonArrayData(testFile);

        actual.AssertDeepEquals(expected);
    }

    [Then("the school expenditure response should be ok, contain a JSON object and match the expected output of '(.*)'")]
    public async Task ThenTheSchoolResponseShouldBeOkAnObjectAndMatchTheExpected(string testFile)
    {
        var response = api[SchoolExpenditureKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var actual = JObject.Parse(content);

        var expected = TestDataProvider.GetJsonObjectData(testFile);

        actual.AssertDeepEquals(expected);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}