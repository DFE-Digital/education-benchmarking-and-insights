using System;
using System.Diagnostics;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using EducationBenchmarking.Platform.Api.Establishments;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.Establishments;

/// <summary>
/// 
/// </summary>
public class Startup : FunctionsStartup
{
    /// <inheritdoc />
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var assemblyDetails = FileVersionInfo.GetVersionInfo(assembly.Location);
        builder.AddSwashBuckle(assembly, opts =>
        {
            opts.AddCodeParameter = true;
            opts.SpecVersion = OpenApiSpecVersion.OpenApi2_0;
            opts.Documents = new[]
            {
                new SwaggerDocument
                {
                    Version = assemblyDetails.ProductVersion ?? string.Empty,
                    Title = assemblyDetails.ProductName ?? string.Empty,
                    Description = assemblyDetails.FileDescription ?? string.Empty
                }
            };
            opts.ConfigureSwaggerGen = x =>
            {
                x.CustomSchemaIds(type => type.ToString());
                x.CustomOperationIds(apiDesc => apiDesc.TryGetMethodInfo(out var mInfo)
                    ? mInfo.Name
                    : default(Guid).ToString());
            };
        });

        builder.Services.AddHealthChecks();
    }
}