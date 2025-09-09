using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.Insight.OpenApi.Examples;

[ExcludeFromCodeCoverage]
internal class ExampleCostCategories : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var status in CostCategories.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(status, status, namingStrategy));

        }

        return this;
    }
}

[ExcludeFromCodeCoverage]
internal class ExampleRagStatuses : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var status in RagRating.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(status, status, namingStrategy));

        }

        return this;
    }
}

[ExcludeFromCodeCoverage]
internal class ExampleFinanceTypes : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var type in FinanceType.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(type, type, namingStrategy));

        }

        return this;
    }
}