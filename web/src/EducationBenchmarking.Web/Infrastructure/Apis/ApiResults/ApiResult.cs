using System.Net;
using EducationBenchmarking.Web.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public abstract class ApiResult(HttpStatusCode status)
{
    public HttpStatusCode Status { get; } = status;

    public bool IsSuccess => (int)Status < 400;

    public static ApiResult Ok<T>(T payload) => new SuccessApiResult(HttpStatusCode.OK, new JsonResponseBody(payload.ToJsonByteArray()));

    public static ApiResult Ok<T>(PagedResults<T> payload) => new SuccessApiResult(HttpStatusCode.OK, new PagedJsonResponseBody(payload.ToJsonByteArray()));

    public static ApiResult BadRequest(params ValidationError[] errors) => new BadRequestApiResult(new JsonResponseBody(errors));

    public static ApiResult Ok() => new SuccessApiResult(HttpStatusCode.OK, new EmptyResponseBody());

    public static ApiResult NotFound() => new NotFoundApiResult();

    public static ApiResult Conflict(ConflictData conflict) => new ConflictApiResult(new JsonResponseBody(conflict));

    public bool EnsureSuccess()
    {
        if (IsSuccess) return true;

        if (this is BadRequestApiResult b && b.Errors.Any())
        {
            throw new ValidationException(b.Errors.Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage)));
        }

        if (this is ConflictApiResult c)
        {
            throw new DataConflictException(c.Details);
        }


        throw new StatusCodeException(Status);
    }

    public static async Task<ApiResult> FromHttpResponse(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content);
            return new SuccessApiResult(response.StatusCode, body);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content);
            return new BadRequestApiResult(body);
        }

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content);
            return new ConflictApiResult(body);
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new NotFoundApiResult();
        }

        if ((int)response.StatusCode >= 500)
        {
            return new ErrorApiResult(response.StatusCode);
        }

        throw new StatusCodeException(response.StatusCode);
    }
}