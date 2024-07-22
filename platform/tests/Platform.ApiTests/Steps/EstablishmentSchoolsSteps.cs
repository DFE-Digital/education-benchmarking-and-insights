using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.Schools;
using Platform.ApiTests.Drivers;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class EstablishmentSchoolsSteps
{
    private const string RequestKey = "get-school";
    private const string SuggestRequestKey = "suggest-school";
    private const string QueryRequestKey = "query-school";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentSchoolsSteps(EstablishmentApiDriver api)
    {
        _api = api;
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

    [Given("an invalid school request with id '(.*)'")]
    private void GivenAnInvalidValidSchoolRequestWithId(string id)
    {
        _api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid schools suggest request with searchText '(.*)")]
    private void GivenAValidSchoolsSuggestRequest(string searchText)
    {
        var content = new { SearchText = searchText, Size = 5, SuggesterName = "school-suggester" };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid schools suggest request")]
    private void GivenAnInvalidSchoolsSuggestRequest()
    {
        var content = new { Size = 0 };

        _api.CreateRequest(SuggestRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("a valid query schools request phase '(.*)' laCode '(.*)' and companyNumber '(.*)'")]
    private void GivenAValidSchoolsQueryRequest(string phase, string laCode, string companyNumber)
    {
        const string baseApiUrl = "/api/schools";
        var queryString = new StringBuilder();

        if (!string.IsNullOrEmpty(companyNumber))
        {
            queryString.Append($"companyNumber={companyNumber}");
        }

        if (!string.IsNullOrEmpty(laCode))
        {
            if (queryString.Length > 0)
            {
                queryString.Append('&');
            }
            queryString.Append($"laCode={laCode}");
        }

        if (!string.IsNullOrEmpty(phase))
        {
            if (queryString.Length > 0)
            {
                queryString.Append('&');
            }
            queryString.Append($"phase={phase}");
        }

        var apiQuery = baseApiUrl + "?" + queryString;

        _api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri(apiQuery, UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the schools request")]
    private async Task WhenISubmitTheSchoolsRequest()
    {
        await _api.Send();
    }

    [Then("the school result should be ok and have the following values:")]
    private async Task ThenTheSchoolResultShouldHaveValues(Table table)
    {
        var response = _api[RequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<School>();
        
        table.CompareToInstance(result);
    }

    [Then("the school result should be not found")]
    private void ThenTheSchoolResultShouldBeNotFound()
    {
        var result = _api[RequestKey].Response;

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the school suggest result should be ok and have the following values:")]
    private async Task ThenTheSchoolsSuggestResultShouldShouldHaveValues(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results;
        var result = results.FirstOrDefault();
        result.Should().NotBeNull();
        
        var actual = new
        {
            result?.Text,
            result?.Document?.SchoolName,
            result?.Document?.URN
        };
       
        table.CompareToInstance(actual);
    }

    [Then("the schools suggest result should be ok and have the following multiple values:")]
    private async Task ThenTheSchoolsSuggestResultShouldShouldHaveMultipleValues(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results.ToList();
        
        var set = results.Select(result => new 
        {
            result.Text,
            result.Document?.SchoolName,
            result.Document?.URN
        }).ToList();
        
        table.CompareToSet(set);
    }

    [Then("the schools suggest result should be empty")]
    private async Task ThenTheSchoolsSuggestResultShouldBeEmpty()
    {
        var response = _api[SuggestRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestResponse<School>>().Results;

        results.Should().BeEmpty();
    }

    [Then("the schools suggest result should be bad request and have the following validation errors:")]
    private async Task ThenTheSchoolsSuggestResultShouldHaveTheFollowingValidationErrors(Table table)
    {
        var response = _api[SuggestRequestKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();
        
        table.CompareToSet(results);
    }

    [Then("the schools query result should be ok and have the following values:")]
    private async Task ThenTheSchoolsQueryResultShouldHaveValues(Table table)
    {
        var response = _api[QueryRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<School[]>();
        
        table.CompareToSet(results);
    }

    [Then("the schools query result should be empty")]
    private async Task ThenTheSchoolsQueryResultShouldBeEmpty()
    {
        var response = _api[QueryRequestKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<School[]>();

        results.Should().BeEmpty();
    }
}