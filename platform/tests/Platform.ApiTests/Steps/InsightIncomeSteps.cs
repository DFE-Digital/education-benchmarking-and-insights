using System.Net;
using FluentAssertions;
using Platform.Api.Insight.Income;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightIncomeSteps(InsightApiDriver api)
{
    private const string SchoolIncomeKey = "school-income";
    private const string TrustIncomeKey = "trust-income";

    [Given("a valid school income dimension request")]
    public void GivenAValidSchoolIncomeDimensionRequest()
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/dimensions", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school income category request")]
    public void GivenAValidSchoolIncomeCategoryRequest()
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/categories", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school income request with urn '(.*)', category '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidSchoolIncomeRequestWithUrnCategoryDimensionAndExcludeCentralServices(
        string urn,
        string category,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/income/school/{urn}?category={category}&dimension={dimension}&excludeCentralServices={excludeCentralServices}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid school income request with urn '(.*)'")]
    public void GivenAnInvalidSchoolIncomeRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school income history request with urn '(.*)'")]
    public void GivenAValidSchoolIncomeHistoryRequestWithUrn(string urn)
    {
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/school/{urn}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid school income query request with urns:")]
    public void GivenAValidSchoolIncomeQueryRequestWithUrns(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(SchoolIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/schools?urns={string.Join("&urns=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust income request with company number '(.*)', category '(.*)', dimension '(.*)' and exclude central services = '(.*)'")]
    public void GivenAValidTrustIncomeRequestWithCompanyNumberCategoryDimensionAndExcludeCentralServices(
        string companyNumber,
        string category,
        string dimension,
        string excludeCentralServices)
    {
        api.CreateRequest(TrustIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri(
                $"/api/income/trust/{companyNumber}?category={category}&dimension={dimension}&excludeCentralServices={excludeCentralServices}",
                UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("an invalid trust income request with company number '(.*)'")]
    public void GivenAnInvalidTrustIncomeRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/trust/{companyNumber}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust income history request with company number '(.*)'")]
    public void GivenAValidTrustIncomeHistoryRequestWithCompanyNumber(string companyNumber)
    {
        api.CreateRequest(TrustIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/trust/{companyNumber}/history", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid trust income query request with company numbers:")]
    public void GivenAValidTrustIncomeQueryRequestWithCompanyNumbers(Table table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(TrustIncomeKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/income/trusts?companyNumbers={string.Join("&companyNumbers=", urns)}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the insights income request")]
    public async Task WhenISubmitTheInsightsIncomeRequest()
    {
        await api.Send();
    }

    [Then("the income dimensions result should be ok and contain:")]
    public async Task ThenTheIncomeDimensionsResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { Dimension = result });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the income categories result should be ok and contain:")]
    public async Task ThenTheIncomeCategoriesResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<string[]>();

        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { Category = result });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the school income result should be ok and contain:")]
    public async Task ThenTheSchoolIncomeResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolIncomeResponse>();
        table.CompareToInstance(result);
    }

    [Then("the school income result should be not found")]
    public void ThenTheSchoolIncomeResultShouldBeNotFound()
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school income history result should be ok and contain:")]
    public async Task ThenTheSchoolIncomeHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolIncomeHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the school income query result should be ok and contain:")]
    public async Task ThenTheSchoolIncomeQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[SchoolIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolIncomeResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust income result should be ok and contain:")]
    public async Task ThenTheTrustIncomeResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustIncomeResponse>();
        table.CompareToInstance(result);
    }

    [Then("the trust income result should be not found")]
    public void ThenTheTrustIncomeResultShouldBeNotFound()
    {
        var response = api[TrustIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust income history result should be ok and contain:")]
    public async Task ThenTheTrustIncomeHistoryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustIncomeHistoryResponse[]>();
        table.CompareToSet(result);
    }

    [Then("the trust income query result should be ok and contain:")]
    public async Task ThenTheTrustIncomeQueryResultShouldBeOkAndContain(Table table)
    {
        var response = api[TrustIncomeKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustIncomeResponse[]>();
        table.CompareToSet(result);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(Table table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}