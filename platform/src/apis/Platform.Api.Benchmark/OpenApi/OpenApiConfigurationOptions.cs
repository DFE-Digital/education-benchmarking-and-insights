using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using Platform.Functions.OpenApi;
namespace Platform.Api.Benchmark.OpenApi;

// ReSharper disable once UnusedType.Global
[ExcludeFromCodeCoverage]
internal class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    private static FileVersionInfo AssemblyDetails => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
    public override OpenApiInfo Info { get; set; } = OpenApiConfiguration.GetOpenApiInfo(AssemblyDetails);
    public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}