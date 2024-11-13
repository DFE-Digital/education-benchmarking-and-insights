using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Api.Insight.Domain;
using Platform.Api.Insight.Expenditure;
namespace Platform.Api.Insight.OpenApi.Examples;

[ExcludeFromCodeCoverage]
internal class ExampleExpenditureCategory : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in ExpenditureCategories.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}

[ExcludeFromCodeCoverage]
internal class ExampleExpenditureDimension : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in ExpenditureDimensions.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}

[ExcludeFromCodeCoverage]
internal class ExampleOverallPhase : OpenApiExample<string>
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