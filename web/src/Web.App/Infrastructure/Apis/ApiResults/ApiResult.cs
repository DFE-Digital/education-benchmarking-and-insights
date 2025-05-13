using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Web.App.Extensions;
namespace Web.App.Infrastructure.Apis;

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

    public static ApiResult Cancelled() => new CancelledApiResult();

    public bool EnsureSuccess()
    {
        if (IsSuccess)
        {
            return true;
        }

        if (this is BadRequestApiResult b && b.Errors.Any())
        {
            throw new ValidationException(b.Errors.Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage)));
        }

        if (this is ConflictApiResult c)
        {
            throw new DataConflictException(c.Details);
        }

        // do not trigger exception handler for 499 Client Closed Request where
        // the original request would have already been discarded by the client
        if (this is CancelledApiResult)
        {
            return true;
        }

        throw new StatusCodeException(Status);
    }

    public static async Task<ApiResult> FromHttpResponse(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        if (response.IsSuccessStatusCode)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content, cancellationToken);
            return new SuccessApiResult(response.StatusCode, body);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content, cancellationToken);
            return new BadRequestApiResult(body);
        }

        if (response.StatusCode == HttpStatusCode.Conflict)
        {
            var body = await ApiResponseBody.FromHttpContent(response.Content, cancellationToken);
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

        // do not trigger exception handler for 499 Client Closed Request where
        // the original request would have already been discarded by the client
        if ((int)response.StatusCode == 499)
        {
            return new CancelledApiResult();
        }

        throw new StatusCodeException(response.StatusCode);
    }
}