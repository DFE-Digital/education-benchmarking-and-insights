using System.Net;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightSchoolsSteps : InsightSteps
{
    private const string GetSchoolFinancesKey = "get-school-finances";
    private const string GetSchoolWorkforceKey = "get-school-workforce";

    public InsightSchoolsSteps(ITestOutputHelper output) : base(output)
    {
    }

    [When(@"I submit the schools insights request")]
    public async Task WhenISubmitTheSchoolsInsightsRequest()
    {
        await Api.Send();
    }

    [Then(@"the school expenditure result should be ok")]
    public async Task ThenTheSchoolExpenditureResultShouldBeOk()
    {
        var response = Api[GetSchoolFinancesKey].Response ??
                       throw new NullException(Api[GetSchoolFinancesKey].Response);
        response.Should().NotBeNull().And.Subject.StatusCode.Should().Be(HttpStatusCode.OK);

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(jsonString);

        json.Should().ContainKey("results");
        var resultsArray = json["results"]?.ToObject<JArray>();
        resultsArray.Should().NotBeNull().And.NotBeEmpty();

        var result = resultsArray!.First!.ToObject<SchoolExpenditure>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Wells Free School");
        result.Urn.Should().Be("139696");

        result = resultsArray[1].ToObject<SchoolExpenditure>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Hadlow Rural Community School");
        result.Urn.Should().Be("139697");
    }

    [Given(@"a valid schools expenditure request with urn '(.*)' and '(.*)'")]
    public void GivenAValidSchoolsExpenditureRequestWithUrnAnd(string urn1, string urn2)
    {
        Api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn1}%2c{urn2}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(@"a valid school expenditure request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithPageAndUrn(string size, string urn)
    {
        Api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&page={size}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the schools expenditure result should be page '(.*)' with '(.*)' page size")]
    public async Task ThenTheSchoolsExpenditureResultShouldBePageWithPageSize(int page, int pageSize)
    {
        var response = Api[GetSchoolFinancesKey].Response ??
                       throw new NullException(Api[GetSchoolFinancesKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<PagedResults<Finances>>() ?? throw new NullException(content);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
    }

    [Given(@"a valid school expenditure request with size '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithSizeAndUrn(string pageSize, string urn)
    {
        Api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(@"a valid schools workforce request with urn '(.*)'")]
    public void GivenAValidSchoolsWorkforceRequestWithUrn(string urn)
    {
        Api.CreateRequest(GetSchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the school workforce result should be ok")]
    public async Task ThenTheSchoolWorkforceResultShouldBeOk()
    {
        var response = Api[GetSchoolWorkforceKey].Response ??
                       throw new NullException(Api[GetSchoolFinancesKey].Response);
        response.Should().NotBeNull().And.Subject.StatusCode.Should().Be(HttpStatusCode.OK);

        var jsonString = await response.Content.ReadAsStringAsync();
        var json = JObject.Parse(jsonString);

        json.Should().ContainKey("results");
        var resultsArray = json["results"]?.ToObject<JArray>();
        resultsArray.Should().NotBeNull().And.NotBeEmpty();

        var result = resultsArray!.First!.ToObject<SchoolWorkforce>();
        result.Should().NotBeNull();
        result!.Name.Should().Be("Wells Free School");
        result.Urn.Should().Be("139696");
    }

    [Given(@"a valid school workforce request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolWorkforceRequestWithPageAndUrn(string page, string urn)
    {
        Api.CreateRequest(GetSchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}&page={page}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the schools workforce result should be page '(.*)' with '(.*)' page size")]
    public async Task ThenTheSchoolsWorkforceResultShouldBePageWithPageSize(int page, int pageSize)
    {
        var response = Api[GetSchoolWorkforceKey].Response ??
                       throw new NullException(Api[GetSchoolWorkforceKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        //todo troubleshoot the reason of failure 
        var result = content.FromJson<PagedResults<SchoolWorkforce>>() ?? throw new NullException(content);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
    }

    [Given(@"a valid school workforce request with size '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolWorkforceRequestWithSizeAndUrn(string pageSize, string urn)
    {
        Api.CreateRequest(GetSchoolWorkforceKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/workforce?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}