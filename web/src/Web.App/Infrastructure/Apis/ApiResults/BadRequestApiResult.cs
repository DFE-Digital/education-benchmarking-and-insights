using System.Net;
using Web.App.Extensions;

namespace Web.App.Infrastructure.Apis;

public sealed class BadRequestApiResult(ApiResponseBody body) : ApiResult(HttpStatusCode.BadRequest)
{
    public ApiResponseBody Body { get; } = body;

    public IEnumerable<ValidationError> Errors => Body.HasContent ? Body.Content.FromJson<ValidationError[]>() ?? Array.Empty<ValidationError>() : Array.Empty<ValidationError>();

}