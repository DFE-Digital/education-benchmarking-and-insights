using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Test.Extensions;

public static class HttpResponseDataExtensions
{
    public static string? ContentType(this HttpResponseData? response)
    {
        ArgumentNullException.ThrowIfNull(response);
        return response.Headers.TryGetValues("Content-Type", out var headers) ? headers.FirstOrDefault() : null;
    }

    public static async Task<T?> ReadAsJsonAsync<T>(this HttpResponseData? response)
    {
        ArgumentNullException.ThrowIfNull(response);
        response.Body.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(response.Body);
    }
}