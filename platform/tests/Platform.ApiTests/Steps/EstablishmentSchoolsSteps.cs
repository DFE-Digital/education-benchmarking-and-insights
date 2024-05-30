using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.Schools;
using Platform.ApiTests.Drivers;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentSchoolsSteps
{
    private const string RequestKey = "get-school";
    private const string NotFoundRequestKey = "get-school-not-found";
    private const string QueryRequestKey = "query-school";
    private const string SearchRequestKey = "search-school";
    private const string SuggestInvalidRequestKey = "suggest-school-invalid";
    private const string SuggestValidRequestKey = "suggest-school-invalid";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentSchoolsSteps(EstablishmentApiDriver api)
    {
        _api = api;
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
        var response = _api[SuggestInvalidRequestKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();
        var set = new List<dynamic>();

        foreach (var result in results)
        {
            set.Add(new { result.PropertyName, result.ErrorMessage });
        }

        table.CompareToDynamicSet(set, false);
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
        var response = _api[SuggestValidRequestKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results;
        var set = new List<dynamic>();

        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.SchoolName, result.Document?.URN });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Given("a valid school request with id '(.*)'")]
    private void GivenAValidSchoolRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the school result should be ok")]
    private async Task ThenTheSchoolResultShouldBeOk()
    {
        var response = _api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<School>();

        result.SchoolName.Should().Be("Burscough Bridge St John's Church of England Primary School");
        result.URN.Should().Be("119376");
    }

    [Given("a invalid school request")]
    private void GivenAInvalidSchoolRequest()
    {
        _api.CreateRequest(NotFoundRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/school/invalid-urn", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the school result should be not found")]
    private void ThenTheSchoolResultShouldBeNotFound()
    {
        var response = _api[NotFoundRequestKey].Response;

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
        var response = _api[QueryRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<PagedResponseModel<School>>();

        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        result.Results?.Count().Should().Be(pageSize);
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
        var response = _api[SearchRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SearchResponse<School>>();

        result.Page.Should().Be(1);
        result.PageSize.Should().Be(15);
        result.Results.Count().Should().Be(15);
    }
}