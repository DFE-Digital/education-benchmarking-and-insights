using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightsSchoolsSteps
{
    private const string GetSchoolFinancesKey = "get-school-finances";
    private readonly ApiDriver _api = new(Config.Apis.Insight ?? throw new NullException(Config.Apis.Insight));

    [When(@"I submit the schools expenditure request")]
    public async Task WhenISubmitTheSchoolsExpenditureRequest()
    {
        await _api.Send();
    }

    [Then(@"the school expenditure result should be ok")]
    public async Task ThenTheSchoolExpenditureResultShouldBeOk()
    {
        var response = _api[GetSchoolFinancesKey].Response ?? throw new NullException(_api[GetSchoolFinancesKey].Response);
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
        _api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn1}%2c{urn2}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given(@"a valid school expenditure request with page '(.*)' and urn '(.*)'")]
    public void GivenAValidSchoolExpenditureRequestWithPageAndUrn(string size, string urn)
    {
        _api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&page={size}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the schools expenditure result should be page '(.*)' with '(.*)' page size")]
    public async Task ThenTheSchoolsExpenditureResultShouldBePageWithPageSize(int page, int pageSize)
    {
        var response = _api[GetSchoolFinancesKey].Response ?? throw new NullException(_api[GetSchoolFinancesKey].Response);

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
        _api.CreateRequest(GetSchoolFinancesKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/expenditure?urns={urn}&pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}