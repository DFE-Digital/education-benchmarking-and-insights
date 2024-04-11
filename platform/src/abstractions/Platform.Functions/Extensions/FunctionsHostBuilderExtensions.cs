using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class FunctionsHostBuilderExtensions
{
    public static IFunctionsHostBuilder AddCustomSwashBuckle(this IFunctionsHostBuilder builder, Assembly assembly)
    {
        var assemblyDetails = FileVersionInfo.GetVersionInfo(assembly.Location);
        builder.AddSwashBuckle(assembly, opts =>
        {
            opts.AddCodeParameter = false;
            opts.SpecVersion = OpenApiSpecVersion.OpenApi3_0;
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

                x.UseAllOfForInheritance();
                x.UseOneOfForPolymorphism();
                x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "x-functions-key",
                    Type = SecuritySchemeType.ApiKey
                });
            };
        });

        return builder;
    }
}