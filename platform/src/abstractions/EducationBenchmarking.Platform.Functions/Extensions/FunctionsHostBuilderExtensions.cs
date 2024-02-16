using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AzureFunctions.Extensions.Swashbuckle;
using AzureFunctions.Extensions.Swashbuckle.Settings;
using EducationBenchmarking.Platform.Domain;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace EducationBenchmarking.Platform.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class FunctionsHostBuilderExtensions
{
    public static IFunctionsHostBuilder AddCustomSwashBuckle(this IFunctionsHostBuilder builder, Assembly assembly)
    {
        var assemblyDetails = FileVersionInfo.GetVersionInfo(assembly.Location);
        builder.AddSwashBuckle(assembly, opts =>
        {
            opts.AddCodeParameter = true;
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
                x.SelectSubTypesUsing(baseType =>
                {
                    if (baseType == typeof(ProximitySort))
                    {
                        return new[]
                        {
                            typeof(BestInClassProximitySort),
                            typeof(SenProximitySort),
                            typeof(SimpleProximitySort)
                        };
                    }

                    return Array.Empty<Type>();
                });
            };
        });

        return builder;
    }
}