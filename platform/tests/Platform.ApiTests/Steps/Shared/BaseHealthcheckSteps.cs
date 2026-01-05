using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Xunit;

namespace Platform.ApiTests.Steps.Shared;

public abstract class BaseHealthcheckSteps(ApiDriver api)
{
    private const string RequestKey = "health-check";

    protected void CreateRequest()
    {
        api.CreateRequest(RequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/health", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    protected async Task SubmitRequest()
    {
        await api.Send();
    }

    protected async Task ValidateResponse()
    {
        var result = api[RequestKey].Response;
        AssertHttpResponse.IsOk(result);

        var content = await result.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", content);
    }
}