using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Functions.OpenApi;

[ExcludeFromCodeCoverage]
public class ExampleYear : OpenApiExample<int>
{
    public override IOpenApiExample<int> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("2022", 2022, namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("2023", 2023, namingStrategy));
        return this;
    }
}


[ExcludeFromCodeCoverage]
public class ExampleDimensionFinance : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in Dimensions.Finance.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}