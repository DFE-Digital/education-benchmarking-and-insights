using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Platform.Functions.OpenApi.Examples;

[ExcludeFromCodeCoverage]
public class ExampleSort : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var sort in new[] { "asc", "desc" })
        {
            Examples.Add(OpenApiExampleResolver.Resolve(sort, sort, namingStrategy));
        }

        return this;
    }
}