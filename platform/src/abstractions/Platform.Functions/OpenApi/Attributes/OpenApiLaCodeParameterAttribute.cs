using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiLaCodeParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiLaCodeParameterAttribute(string name = "laCode", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "Local authority three digit code";
        In = location;
    }
}

