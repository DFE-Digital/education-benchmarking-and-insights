using System.Text.Json;
using Microsoft.Azure.Functions.Worker.Http;
namespace Platform.Tests;

public static class TestExtensions
{
    public static async Task<T?> ReadAsJsonAsync<T>(this HttpResponseData response)
    {
        ArgumentNullException.ThrowIfNull(response);
        response.Body.Position = 0;
        return await JsonSerializer.DeserializeAsync<T>(response.Body);
    }
}