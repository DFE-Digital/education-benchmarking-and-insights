using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiCategoryParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiCategoryParameterAttribute() : base("category")
    {
        Type = typeof(string);
        Required = false;
        Description = "Expenditure/Census category";
        In = ParameterLocation.Query;
    }
}