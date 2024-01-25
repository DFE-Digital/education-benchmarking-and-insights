using System.Net;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class SuccessApiResult(HttpStatusCode status, ApiResponseBody body) : ApiResult(status)
{
    public ApiResponseBody Body { get; } = body;
}