using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiNurseryProvisionParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiNurseryProvisionParameterAttribute(string name = "nurseryProvision", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string[]);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "List of nursery provisions to filter resultant values";
        In = location;
    }
}