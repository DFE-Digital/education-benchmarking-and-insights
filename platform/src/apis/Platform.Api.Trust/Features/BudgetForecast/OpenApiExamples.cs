using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Platform.Api.Trust.Features.BudgetForecast;

[ExcludeFromCodeCoverage]
public static class OpenApiExamples
{
    internal class BudgetForecastRunType : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Default", "default", namingStrategy));
            return this;
        }
    }

    internal class BudgetForecastRunCategory : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("Revenue Reserve", "Revenue reserve", namingStrategy));
            return this;
        }
    }

    internal class BudgetForecastRunId : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add(OpenApiExampleResolver.Resolve("2022", "2022", namingStrategy));
            Examples.Add(OpenApiExampleResolver.Resolve("2023", "2023", namingStrategy));
            return this;
        }
    }
}