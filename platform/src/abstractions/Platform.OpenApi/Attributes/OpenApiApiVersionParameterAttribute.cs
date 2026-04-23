using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiApiVersionParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiApiVersionParameterAttribute(string name = "x-api-version", ParameterLocation location = ParameterLocation.Header, bool isRequired = false) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "The requested API version";
        In = location;
    }
}