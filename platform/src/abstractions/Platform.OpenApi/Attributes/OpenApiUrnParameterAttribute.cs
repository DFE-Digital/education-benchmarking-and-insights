using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiUrnParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiUrnParameterAttribute(string name = "urn", ParameterLocation location = ParameterLocation.Path, bool isRequired = true) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "The 6-digit Unique Reference Number of the school.";
        In = location;
    }
}