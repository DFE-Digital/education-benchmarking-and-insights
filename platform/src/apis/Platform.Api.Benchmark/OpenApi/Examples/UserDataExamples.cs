using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
namespace Platform.Api.Benchmark.OpenApi.Examples;

internal class ExampleUserDataType : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("Comparator set", "comparator-set", namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Custom data", "custom-data", namingStrategy));
        return this;
    }
}

internal class ExampleOrganisationType : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("School", "school", namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Trust", "trust", namingStrategy));
        return this;
    }
}

internal class ExampleUserDataStatus : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("Pending", "pending", namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Complete", "complete", namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("Failed", "failed", namingStrategy));
        return this;
    }
}