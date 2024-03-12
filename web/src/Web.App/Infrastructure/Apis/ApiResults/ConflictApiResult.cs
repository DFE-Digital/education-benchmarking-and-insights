using System.Net;
using Web.App.Extensions;

namespace Web.App.Infrastructure.Apis
{
    public sealed class ConflictApiResult(ApiResponseBody body) : ApiResult(HttpStatusCode.Conflict)
    {
        public ApiResponseBody Body { get; } = body;

        public ConflictData Details => Body.Content.FromJson<ConflictData>() ?? new ConflictData();
    }
}