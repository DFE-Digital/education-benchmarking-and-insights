using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace Platform.OpenApi.Attributes;

[ExcludeFromCodeCoverage]
public sealed class OpenApiOverallPhaseParameterAttribute : OpenApiParameterAttribute
{
    public OpenApiOverallPhaseParameterAttribute(string name = "overallPhase", ParameterLocation location = ParameterLocation.Query, bool isRequired = false) : base(name)
    {
        Type = typeof(string);
        Required = location == ParameterLocation.Path || isRequired;
        Description = "School overall phase";
        In = location;
    }
}