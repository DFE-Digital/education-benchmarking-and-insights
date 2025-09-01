using Platform.Api.Content.Features.News.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content news endpoints")]
public class ContentNewsSteps(ContentApiDriver api)
{
    private const string NewsKey = "News";

    [Given("a news article request for the slug '(.*)'")]
    public void GivenANewsArticleRequestForTheSlug(string slug)
    {
        api.CreateRequest(NewsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/news/{slug}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a news article request with API version '(.*)' for the slug '(.*)'")]
    public void GivenANewsArticleRequestWithApiVersionForTheSlug(string version, string slug)
    {
        GivenANewsArticleRequestForTheSlug(slug);
        api[NewsKey].Request.Headers.Add("x-api-version", version);
    }

    [When("I submit the news request")]
    public async Task WhenISubmitTheNewsRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and equal:")]
    public async Task ThenTheResultShouldBeOkAndEqual(DataTable table)
    {
        var response = api[NewsKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<News>();
        table.CompareToInstance(result);
    }

    [Then("the result should be not found")]
    public void ThenTheResultShouldBeNotFound()
    {
        var response = api[NewsKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        var response = api[NewsKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}