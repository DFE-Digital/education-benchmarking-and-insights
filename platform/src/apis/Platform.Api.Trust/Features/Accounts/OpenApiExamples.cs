using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;

namespace Platform.Api.Trust.Features.Accounts;

[ExcludeFromCodeCoverage]
public static class OpenApiExamples
{
    internal class DimensionFinance : OpenApiExample<string>
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
}