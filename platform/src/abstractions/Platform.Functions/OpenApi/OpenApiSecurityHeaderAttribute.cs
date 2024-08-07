using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public sealed class OpenApiSecurityHeaderAttribute : OpenApiSecurityAttribute
{
    public OpenApiSecurityHeaderAttribute()
        : base("ApiKey", SecuritySchemeType.ApiKey)
    {
        Name = "x-functions-key";
        In = OpenApiSecurityLocationType.Header;
    }
}