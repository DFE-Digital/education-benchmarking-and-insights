using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
namespace Platform.Api.Insight.OpenApi.Examples;

internal class ExampleBudgetForecastRunType : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("Default", "default", namingStrategy));
        return this;
    }
}

internal class ExampleBudgetForecastRunCategory : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("Revenue Reserve", "Revenue reserve", namingStrategy));
        return this;
    }
}

internal class ExampleBudgetForecastRunId : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        Examples.Add(OpenApiExampleResolver.Resolve("2022", "2022", namingStrategy));
        Examples.Add(OpenApiExampleResolver.Resolve("2023", "2023", namingStrategy));
        return this;
    }
}