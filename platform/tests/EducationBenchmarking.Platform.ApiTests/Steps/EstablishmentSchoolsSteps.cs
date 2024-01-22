using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentSchoolsSteps
{
    private const string GetRequestKey = "get-school";
    private const string GetNotFoundRequestKey = "get-school-not-found";
    private const string QueryRequestKey = "query-school";
    private const string SearchRequestKey = "search-school";
    private const string SuggestInvalidRequestKey = "suggest-school-invalid";
    private const string SuggestValidRequestKey = "suggest-school-invalid";
    private readonly ApiDriver _api;
    
    public EstablishmentSchoolsSteps(ITestOutputHelper output)
    {
        _api = new ApiDriver(Config.Apis.Establishment ?? throw new NullException(Config.Apis.Establishment), output);
    }
    
    [Given("an invalid schools suggest request")]
    private void GivenAnInvalidSchoolsSuggestRequest()
    {
        var content = new { Size = 0 };
        
        _api.CreateRequest(SuggestInvalidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the schools request")]
    private async Task WhenISubmitTheSchoolsRequest()
    {
        await _api.Send();
    }

    [Then("the schools suggest result should have the follow validation errors:")]
    private async Task ThenTheSchoolsSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = _api[SuggestInvalidRequestKey].Response ?? throw new NullException(_api[SuggestInvalidRequestKey].Response);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        
        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>() ?? throw new NullException(content);
        
        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { result.PropertyName, result.ErrorMessage });
        }
        
        table.CompareToDynamicSet(set,false);
    }

    [Given("a valid schools suggest request")]
    private void GivenAValidSchoolsSuggestRequest()
    {
        var content = new { SearchText = "school", Size = 10, SuggesterName = "school-suggester" };
        
        _api.CreateRequest(SuggestValidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }
    
    [Then("the schools suggest result should be:")]
    private async Task ThenTheSchoolsSuggestResultShouldBe(Table table)
    {
        var response = _api[SuggestValidRequestKey].Response ?? throw new NullException(_api[SuggestValidRequestKey].Response);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestOutput<School>>()?.Results ?? throw new NullException(content);
        
        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.Name, result.Document?.Urn  });
        }

        table.CompareToDynamicSet(set,false);
    }

    [Given("a valid school request with id '(.*)'")]
    private void GivenAValidSchoolRequestWithId(string id)
    {
        _api.CreateRequest(GetRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the school result should be ok")]
    private async Task ThenTheSchoolResultShouldBeOk()
    {
        var response = _api[GetRequestKey].Response ?? throw new NullException(_api[GetRequestKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<School>() ?? throw new NullException(content);
        result.Name.Should().Be("Burscough Bridge St John's Church of England Primary School");
        result.Urn.Should().Be("119376");
    }

    [Given("a invalid school request")]
    private void GivenAInvalidSchoolRequest()
    {
        _api.CreateRequest(GetNotFoundRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/school/invalid-urn", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the school result should be not found")]
    private void ThenTheSchoolResultShouldBeNotFound()
    {
        var response = _api[GetNotFoundRequestKey].Response ?? throw new NullException(_api[GetNotFoundRequestKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Given("a valid schools query request with page '(.*)'")]
    private void GivenAValidSchoolsQueryRequestWithPage(string page)
    {
        _api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools?page={page}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the schools query result should be page '(.*)' with '(.*)' records")]
    private async Task ThenTheSchoolsQueryResultShouldBePageWithRecords(int page, int pageSize)
    {
        var response = _api[QueryRequestKey].Response ?? throw new NullException(_api[QueryRequestKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<PagedResults<School>>() ?? throw new NullException(content);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        result.Results.Count().Should().Be(pageSize);
    }

    [Given("a valid schools query request with page size '(.*)'")]
    private void GivenAValidSchoolsQueryRequestWithPageSize(int pageSize)
    {
        _api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools?pageSize={pageSize}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid schools search request")]
    private void GivenAValidSchoolsSearchRequest()
    {
        var content = new { };
        _api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/search", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the schools search result should be ok")]
    private async Task ThenTheSchoolsSearchResultShouldBeOk()
    {
        var response = _api[SearchRequestKey].Response ?? throw new NullException(_api[SearchRequestKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SearchOutput<School>>() ?? throw new NullException(content);

        result.Page.Should().Be(1);
        result.PageSize.Should().Be(15);
        result.Results.Count().Should().Be(15);
    }
}