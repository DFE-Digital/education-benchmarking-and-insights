using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiLaCodesParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiLaCodesParameterAttribute(string name = "laCodes", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string[]);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "List of local authority three digit codes";
        In = location;
    }
}