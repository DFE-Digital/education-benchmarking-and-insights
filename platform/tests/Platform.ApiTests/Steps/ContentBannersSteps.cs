using Platform.Api.Content.Features.Banners.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content banners endpoints")]
public class ContentBannersSteps(ContentApiDriver api)
{
    private const string BannersKey = "banners";

    [Given("a banners request for the target '(.*)'")]
    public void GivenABannersRequestForTheTarget(string target)
    {
        api.CreateRequest(BannersKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/banner/{target}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a banners request with API version '(.*)' for the target '(.*)'")]
    public void GivenABannersRequestWithApiVersionForTheTarget(string version, string target)
    {
        GivenABannersRequestForTheTarget(target);
        api[BannersKey].Request.Headers.Add("x-api-version", version);
    }

    [When("I submit the banners request")]
    public async Task WhenISubmitTheBannersRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and equal:")]
    public async Task ThenTheResultShouldBeOkAndEqual(DataTable table)
    {
        var response = api[BannersKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Banner>();
        table.CompareToInstance(result);
    }

    [Then("the result should be not found")]
    public void ThenTheResultShouldBeNotFound()
    {
        var response = api[BannersKey].Response;
        AssertHttpResponse.IsNotFound(response);
    }

    [Then("the result should be bad request")]
    public void ThenTheResultShouldBeBadRequest()
    {
        var response = api[BannersKey].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}