using System.Net;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class ErrorApiResult : ApiResult
{
    public ErrorApiResult(HttpStatusCode status) : base(status)
    {
    }
}