using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions;
namespace Platform.Api.Benchmark.Extensions;

public static class HttpRequestDataExtensions
{
    public static Guid GetCorrelationId(this HttpRequestData req)
    {
        if (req.Headers.TryGetValues(Functions.Constants.CorrelationIdHeader, out var values))
        {
            return Guid.TryParse(values.ToString(), out var guid)
                ? guid
                : Guid.NewGuid();
        }

        return Guid.NewGuid();
    }

    public static async Task<T> ReadAsJsonAsync<T>(this HttpRequestData req) => await req.ReadFromJsonAsync<T>() ?? throw new ArgumentNullException();

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
        response.Headers.Add("Content-Type", "text/plain");
        await response.WriteBytesAsync(bytes);
        return response;
    }
}