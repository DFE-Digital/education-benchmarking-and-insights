using System.Net;
using System.Text;
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
public class EstablishmentTrustsSteps : EstablishmentSteps
{
    private const string GetRequestKey = "get-trust";
    private const string QueryRequestKey = "query-trust";
    private const string SearchRequestKey = "search-trust";
    private const string SuggestInvalidRequestKey = "suggest-trust-invalid";
    private const string SuggestValidRequestKey = "suggest-trust-invalid";

    public EstablishmentTrustsSteps(ITestOutputHelper output) : base(output)
    {
    }

    [Given("a valid trust request with id '(.*)'")]
    private void GivenAValidTrustRequestWithId(string id)
    {
        Api.CreateRequest(GetRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{id}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the trusts request")]
    private async Task WhenISubmitTheTrustsRequest()
    {
        await Api.Send();
    }

    [Then("the trust result should be ok")]
    private void ThenTheTrustResultShouldBeOk()
    {
        var result = Api[GetRequestKey].Response ?? throw new NullException(Api[GetRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid trusts query request")]
    private void GivenAValidTrustsQueryRequest()
    {
        Api.CreateRequest(QueryRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the trusts query result should be ok")]
    private void ThenTheTrustsQueryResultShouldBeOk()
    {
        var result = Api[QueryRequestKey].Response ?? throw new NullException(Api[QueryRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("a valid trusts search request")]
    private void GivenAValidTrustsSearchRequest()
    {
        Api.CreateRequest(SearchRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/search", UriKind.Relative),
            Method = HttpMethod.Post
        });
    }

    [Then("the trusts search result should be ok")]
    private void ThenTheTrustsSearchResultShouldBeOk()
    {
        var result = Api[SearchRequestKey].Response ?? throw new NullException(Api[SearchRequestKey].Response);

        result.Should().NotBeNull();
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("an invalid trusts suggest request")]
    private void GivenAnInvalidTrustsSuggestRequest()
    {
        var content = new { Size = 0 };

        Api.CreateRequest(SuggestInvalidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the trusts suggest result should have the follow validation errors:")]
    private async Task ThenTheTrustsSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = Api[SuggestInvalidRequestKey].Response ??
                       throw new NullException(Api[SuggestInvalidRequestKey].Response);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>() ?? throw new NullException(content);

        var set = new List<dynamic>();
        foreach (var result in results) set.Add(new { result.PropertyName, result.ErrorMessage });

        table.CompareToDynamicSet(set, false);
    }

    [Given("a valid trusts suggest request")]
    private void GivenAValidTrustsSuggestRequest()
    {
        var content = new { SearchText = "trust", Size = 10, SuggesterName = "trust-suggester" };

        Api.CreateRequest(SuggestValidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/trusts/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the trusts suggest result should be:")]
    private async Task ThenTheTrustsSuggestResultShouldBe(Table table)
    {
        var response = Api[SuggestValidRequestKey].Response ??
                       throw new NullException(Api[SuggestValidRequestKey].Response);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestOutput<Trust>>()?.Results ?? throw new NullException(content);

        var set = new List<dynamic>();
        foreach (var result in results)
            set.Add(new { result.Text, result.Document?.Name, result.Document?.CompanyNumber });

        table.CompareToDynamicSet(set, false);
    }
}