using System.Net;
using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class ConflictApiResult : ApiResult
{
    public ApiResponseBody Body { get; }

    public ConflictApiResult(ApiResponseBody body) : base(HttpStatusCode.Conflict)
    {
        Body = body;
    }

    public ConflictData Details => Body.Content.FromJson<ConflictData>() ?? new ConflictData();
}