using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps.Shared;

public abstract class BaseHealthcheckSteps(ApiDriver api)
{
    protected const string RequestKey = "health-check";

    [Given("a valid request")]
    protected void CreateRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    protected async Task SubmitRequest()
    {
        await api.Send();
    }

    [Then("the result should be '(.*)'")]
    protected async Task ValidateResponse(string status)
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);
        Assert.Equal("text/plain", result.Content.Headers.ContentType?.MediaType);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal(status, content);
    }
}