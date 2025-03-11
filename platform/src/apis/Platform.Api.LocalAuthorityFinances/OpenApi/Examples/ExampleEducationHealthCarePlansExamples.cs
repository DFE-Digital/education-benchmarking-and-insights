using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.LocalAuthorityFinances.OpenApi.Examples;

[ExcludeFromCodeCoverage]
internal class ExampleHighNeedsDimension : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in Dimensions.HighNeeds.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}