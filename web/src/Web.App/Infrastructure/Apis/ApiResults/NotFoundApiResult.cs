using System.Net;

namespace Web.App.Infrastructure.Apis
{
    public sealed class NotFoundApiResult() : ApiResult(HttpStatusCode.NotFound);
}