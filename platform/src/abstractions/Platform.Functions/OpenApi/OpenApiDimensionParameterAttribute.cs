using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiDimensionParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiDimensionParameterAttribute() : base("dimension")
    {
        Type = typeof(string);
        Required = false;
        Description = "Dimension for response values";
        In = ParameterLocation.Query;
    }
}