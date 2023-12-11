using System.Net;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class SuccessApiResult : ApiResult
{
    public ApiResponseBody Body { get; }

    public SuccessApiResult(HttpStatusCode status, ApiResponseBody body) : base(status)
    {
        Body = body;
    }
}