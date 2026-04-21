using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiUrnParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiUrnParameterAttribute() : base("urn")
    {
        Type = typeof(string);
        Required = true;
        Description = "The 6-digit Unique Reference Number of the school.";
        In = ParameterLocation.Path;
    }
}