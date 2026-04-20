using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiUrnsParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiUrnsParameterAttribute() : base("urns")
    {
        Type = typeof(string[]);
        Required = true;
        Description = "List of school URNs";
        In = ParameterLocation.Query;
    }
}