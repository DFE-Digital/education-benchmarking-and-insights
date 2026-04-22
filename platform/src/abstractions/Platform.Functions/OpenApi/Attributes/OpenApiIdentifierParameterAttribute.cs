using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.Functions.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiIdentifierParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiIdentifierParameterAttribute(string name = "identifier", ParameterLocation location = ParameterLocation.Path, bool isRequired = true) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "The identifier of the user defined comparator set.";
        In = location;
    }
}

