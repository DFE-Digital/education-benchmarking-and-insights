using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Expenditure;
using Platform.ApiTests.Assist;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using Platform.Json;

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

    [Then("the expenditure dimensions result should be ok and contain:")]
    public async Task ThenTheExpenditureDimensionsResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new
            {
                Dimension = result
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the expenditure categories result should be ok and contain:")]
    public async Task ThenTheExpenditureCategoriesResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new
            {
                Category = result
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the school expenditure result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureSchoolResponse>();
        table.CompareToInstance(result);
    }

    [Then("the school expenditure result should be not found")]
    public void ThenTheSchoolExpenditureResultShouldBeNotFound()
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school expenditure result should be bad request")]
    public void ThenTheSchoolExpenditureResultShouldBeBadRequest()
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Then("the school expenditure history result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the school expenditure query result should be ok and contain:")]
    public async Task ThenTheSchoolExpenditureQueryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[SchoolExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureSchoolResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust expenditure result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureTrustResponse>();
        table.CompareToInstance(result);
    }

    [Then("the trust expenditure result should be not found")]
    public void ThenTheTrustExpenditureResultShouldBeNotFound()
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust expenditure result should be bad request")]
    public void ThenTheTrustExpenditureResultShouldBeBadRequest()
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Then("the trust expenditure history result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureHistoryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureHistoryResponse>();
        table.CompareToSet(result.Rows);
    }

    [Then("the trust expenditure query result should be ok and contain:")]
    public async Task ThenTheTrustExpenditureQueryResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[TrustExpenditureKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ExpenditureTrustResponse[]>();
        table.CompareToSet(result);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}