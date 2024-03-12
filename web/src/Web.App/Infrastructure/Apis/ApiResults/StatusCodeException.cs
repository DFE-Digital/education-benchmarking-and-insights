using System.Net;
using Web.App.Extensions;

namespace Web.App.Infrastructure.Apis
{
    public class StatusCodeException(HttpStatusCode statusCode)
        : Exception($"The api returned `{GetFriendlyMessage(statusCode)}` underlying status code: {statusCode}")
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
                _ => status.ToString().SplitPascalCase()
            };
        }
    }
}