using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiCategoryParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiCategoryParameterAttribute(string name = "category", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "Expenditure/Census category";
        In = location;
    }
}