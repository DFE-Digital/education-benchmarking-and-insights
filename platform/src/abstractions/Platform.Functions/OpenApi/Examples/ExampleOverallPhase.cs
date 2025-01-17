using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Functions.OpenApi.Examples;

[ExcludeFromCodeCoverage]
public class ExampleOverallPhase : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var phase in OverallPhase.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(phase, phase, namingStrategy));
        }

        return this;
    }
}