using System;
using System.Diagnostics;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using EducationBenchmarking.Platform.Api.AcademyTrust;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.AcademyTrust;

public class Startup : FunctionsStartup
{
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
        });

        builder.Services.AddHealthChecks();
    }
}