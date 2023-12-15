using System;
using System.Diagnostics;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using EducationBenchmarking.Platform.Api.School;
using EducationBenchmarking.Platform.Api.School.Db;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

[assembly: WebJobsStartup(typeof(Startup))]

namespace EducationBenchmarking.Platform.Api.School;

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
        builder.Services.AddSingleton<ISchoolExpenditureDb, SchoolExpenditureDb>();
    }
}