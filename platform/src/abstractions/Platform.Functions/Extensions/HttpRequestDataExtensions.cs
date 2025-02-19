using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using FluentValidation.Results;
using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpRequestDataExtensions
{
    public static Guid GetCorrelationId(this HttpRequestData req)
    {
        if (req.Headers.TryGetValues(Constants.CorrelationIdHeader, out var values))
        {
            return Guid.TryParse(values.FirstOrDefault(), out var guid)
                ? guid
                : Guid.Empty;
        }

        return req.SetCorrelationId();
    }

    private static Guid SetCorrelationId(this HttpRequestData req)
    {
        var guid = Guid.NewGuid();
        req.Headers.Add(Constants.CorrelationIdHeader, guid.ToString());
        return guid;
    }

    public static async Task<T> ReadAsJsonAsync<T>(this HttpRequestData req)
    {
        var result = default(T);
        try
        {
            result = await req.ReadFromJsonAsync<T>();
        }
        catch (Exception)
        {
            // known issue in .NET 8: https://github.com/dotnet/roslyn/issues/72141
        }

        if (result is null)
        {
            throw new ArgumentNullException();
        }

        return result;
    }

    public static T GetParameters<T>(this HttpRequestData req) where T : QueryParameters, new()
    {
        var parameters = new T();
        parameters.SetValues(req.Query);

        return parameters;
    }

    public static async Task<HttpResponseData> CreateJsonResponseAsync(this HttpRequestData req, object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = req.CreateResponse(statusCode);
        await response.WriteAsJsonAsync(obj);
        return response;
    }

    public static async Task<HttpResponseData> CreateObjectResponseAsync(this HttpRequestData req, object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = req.CreateResponse(statusCode);
        var bytes = Encoding.UTF8.GetBytes(obj.ToString() ?? string.Empty);
        response.Headers.Add("Content-Type", ContentType.TextPlain);
        await response.WriteBytesAsync(bytes);
        return response;
    }

    public static async Task<HttpResponseData> CreateValidationErrorsResponseAsync(this HttpRequestData req, IEnumerable<ValidationFailure> failures, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return await req.CreateJsonResponseAsync(failures.Select(e => new ValidationError(e.Severity, e.PropertyName, e.ErrorMessage)), statusCode);
    }

    public static HttpResponseData CreateErrorResponse(this HttpRequestData req, int statusCode = (int)HttpStatusCode.InternalServerError) => req.CreateResponse((HttpStatusCode)statusCode);

    public static HttpResponseData CreateNotFoundResponse(this HttpRequestData req) => req.CreateResponse(HttpStatusCode.NotFound);
}