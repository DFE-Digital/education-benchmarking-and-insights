using System.Net;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class NotFoundApiResult : ApiResult
{
    public NotFoundApiResult() : base(HttpStatusCode.NotFound)
    {
    }
}