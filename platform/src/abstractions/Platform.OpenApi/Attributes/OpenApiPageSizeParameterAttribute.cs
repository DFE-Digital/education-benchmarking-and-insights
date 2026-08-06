using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiPageSizeParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiPageSizeParameterAttribute(string name = "pageSize", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "The number of results to include in each page of a paginated response.";
        In = location;
    }
}
