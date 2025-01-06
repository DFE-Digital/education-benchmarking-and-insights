using System.Net;
using Web.App.Extensions;
namespace Web.App.Infrastructure.Apis;

public class StatusCodeException(HttpStatusCode statusCode)
    : Exception($"The API returned `{GetFriendlyMessage(statusCode)}` (underlying status code {(int)statusCode})")
{
    public HttpStatusCode Status { get; } = statusCode;

    public static string GetFriendlyMessage(HttpStatusCode status)
    {
        return status switch
        {
            HttpStatusCode.Accepted => "Accepted",
            HttpStatusCode.BadRequest => "Bad Request",
            HttpStatusCode.InternalServerError => "Internal Server Error",
            HttpStatusCode.NotFound => "Resource not found",
            HttpStatusCode.NotImplemented => "Not yet implemented",
            HttpStatusCode.OK => "OK",
            (HttpStatusCode)499 => "Client Closed Request",
            _ => status.ToString().SplitPascalCase()
        };
    }
}