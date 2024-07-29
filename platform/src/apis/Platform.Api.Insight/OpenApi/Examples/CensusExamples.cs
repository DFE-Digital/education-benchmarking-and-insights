using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Api.Insight.Census;
namespace Platform.Api.Insight.OpenApi.Examples;

internal class ExampleCensusCategory : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in CensusCategories.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}

internal class ExampleCensusDimension : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in CensusDimensions.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}