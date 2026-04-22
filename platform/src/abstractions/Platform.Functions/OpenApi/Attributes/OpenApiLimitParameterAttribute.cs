using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiLimitParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiLimitParameterAttribute(string name = "limit", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(int);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "Number of records to return. If omitted, all are returned.";
        In = location;
    }
}

