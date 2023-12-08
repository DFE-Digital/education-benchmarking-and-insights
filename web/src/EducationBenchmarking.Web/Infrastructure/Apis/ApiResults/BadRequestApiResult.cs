using System.Net;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public sealed class BadRequestApiResult : ApiResult
{
    public ApiResponseBody Body { get; }

    public BadRequestApiResult(ApiResponseBody body) : base(HttpStatusCode.BadRequest)
    {
        Body = body;
    }

    public IEnumerable<ValidationError> Errors => Body.HasContent ? Body.Content.FromJson<ValidationError[]>() ?? Array.Empty<ValidationError>() : Array.Empty<ValidationError>();
    
}