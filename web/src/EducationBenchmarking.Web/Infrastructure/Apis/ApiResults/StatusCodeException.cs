using System.Net;
using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class StatusCodeException : Exception
{
    public HttpStatusCode Status { get; }

    public StatusCodeException(HttpStatusCode statusCode)
        : base($"The api returned `{GetFriendlyMessage(statusCode)}` underlying status code: {statusCode}")
    {
        Status = statusCode;
    }

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