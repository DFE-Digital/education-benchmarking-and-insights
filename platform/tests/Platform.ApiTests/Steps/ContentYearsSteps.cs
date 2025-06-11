using Platform.Api.Content.Features.Years.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content years endpoints")]
public class ContentYearsSteps(ContentApiDriver api)
{
    private const string Key = "years";

    [Given("a current return years request")]
    public void GivenACurrentReturnYearsRequest()
    {
        api.CreateRequest(Key, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/current-return-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a current return years request with API version '(.*)'")]
    public void GivenACurrentReturnYearsRequestWithApiVersion(string version)
    {
        GivenACurrentReturnYearsRequest();
        api[Key].Request.Headers.Add("x-api-version", version);
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the current return years result should be:")]
    public async Task ThenTheCurrentReturnYearsResultShouldBe(DataTable table)
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        var result = content.FromJson<FinanceYears>();

        table.CompareToInstance(result);
    }

    [Then("the current return years result should be bad request")]
    public void ThenTheCurrentReturnYearsResultShouldBeBadRequest()
    {
        var response = api[Key].Response;
        AssertHttpResponse.IsBadRequest(response);
    }
}