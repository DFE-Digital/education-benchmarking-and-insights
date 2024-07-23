using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;
namespace Platform.Tests.Extensions;

public static class HttpResponseDataExtensions
{
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpResponseData response)
    {
        ArgumentNullException.ThrowIfNull(response);
        response.Body.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(response.Body);
    }
}