using System.Net;
using Xunit;

namespace Platform.ApiTests.Assertion;

public static class AssertHttpResponse
{
    public static void IsAccepted(HttpResponseMessage response)
    {
        response.HasStatusCode(HttpStatusCode.Accepted);
    }

    public static void IsBadRequest(HttpResponseMessage response)
    {
        response.HasStatusCode(HttpStatusCode.BadRequest);
    }

    public static void IsNotFound(HttpResponseMessage response)
    {
        response.HasStatusCode(HttpStatusCode.NotFound);
    }

    public static void IsOk(HttpResponseMessage response)
    {
        response.HasStatusCode(HttpStatusCode.OK);
    }

    public static void IsInternalServerError(HttpResponseMessage response)
    {
        response.HasStatusCode(HttpStatusCode.InternalServerError);
    }

    private static void HasStatusCode(this HttpResponseMessage response, HttpStatusCode statusCode)
    {
        Assert.NotNull(response);
        Assert.Equal(statusCode, response.StatusCode);
    }
}
