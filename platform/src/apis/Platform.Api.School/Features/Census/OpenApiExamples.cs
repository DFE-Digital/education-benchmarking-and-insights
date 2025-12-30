using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.School.Features.Census;

[ExcludeFromCodeCoverage]
public static class OpenApiExamples
{
    public class Dimension : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            foreach (var dimension in Dimensions.Census.All)
            {
                Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
            }

            return this;
        }
    }

    public class Category : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            foreach (var dimension in Categories.Census.All)
            {
                Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
            }

            return this;
        }
    }

    public class Phase : OpenApiExample<string>
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

    internal class FinanceTypes : OpenApiExample<string>
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
}