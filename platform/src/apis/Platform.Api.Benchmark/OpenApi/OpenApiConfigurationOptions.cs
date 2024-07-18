using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
namespace Platform.Api.Benchmark.OpenApi;

// ReSharper disable once UnusedType.Global
internal class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    private static FileVersionInfo AssemblyDetails => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

    public override OpenApiInfo Info { get; set; } = new()
    {
        Version = AssemblyDetails.ProductVersion ?? string.Empty,
        Title = AssemblyDetails.ProductName ?? string.Empty,
        Description = AssemblyDetails.FileDescription ?? string.Empty,
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("http://opensource.org/licenses/MIT")
        }
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}