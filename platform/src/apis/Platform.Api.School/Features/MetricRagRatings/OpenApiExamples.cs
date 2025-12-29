using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.School.Features.MetricRagRatings;

[ExcludeFromCodeCoverage]
public static class OpenApiExamples
{
    public class CategoryCost : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            foreach (var dimension in Categories.Cost.All)
            {
                Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
            }

            return this;
        }
    }

    [ExcludeFromCodeCoverage]
    internal class RagStatuses : OpenApiExample<string>
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

    internal class Phase : OpenApiExample<string>
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
}