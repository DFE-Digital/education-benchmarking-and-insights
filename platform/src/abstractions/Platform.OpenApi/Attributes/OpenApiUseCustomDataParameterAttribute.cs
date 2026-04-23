using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiUseCustomDataParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiUseCustomDataParameterAttribute(string name = "useCustomData", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(bool);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "Sets whether or not to use custom data context";
        In = location;
    }
}