using System.Net;

namespace Web.App.Infrastructure.Apis
{
    public sealed class ErrorApiResult(HttpStatusCode status) : ApiResult(status);
}