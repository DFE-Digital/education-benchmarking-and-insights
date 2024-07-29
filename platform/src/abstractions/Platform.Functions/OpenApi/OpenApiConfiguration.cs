using System.Diagnostics;
using Microsoft.OpenApi.Models;
namespace Platform.Functions.OpenApi;

public static class OpenApiConfiguration
{
    public static OpenApiInfo GetOpenApiInfo(FileVersionInfo fileVersion) => new()
    {
        Version = fileVersion.ProductVersion ?? string.Empty,
        Title = fileVersion.ProductName ?? string.Empty,
        Description = fileVersion.FileDescription ?? string.Empty,
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    };
}