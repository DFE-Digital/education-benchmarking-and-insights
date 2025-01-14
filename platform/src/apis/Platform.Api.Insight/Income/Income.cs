using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace Platform.Api.Insight.Income;

[ExcludeFromCodeCoverage]
public static class IncomeDimensions
{
    public const string Actuals = nameof(Actuals);
    public const string PerUnit = nameof(PerUnit);
    public const string PercentIncome = nameof(PercentIncome);
    public const string PercentExpenditure = nameof(PercentExpenditure);

    public static readonly string[] All =
    [
        Actuals,
        PerUnit,
        PercentIncome,
        PercentExpenditure
    ];

    public static bool IsValid(string? dimension) => All.Any(a => a == dimension);
}

[ExcludeFromCodeCoverage]
public class ExampleIncomeDimension : OpenApiExample<string>
{
    public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
    {
        foreach (var dimension in IncomeDimensions.All)
        {
            Examples.Add(OpenApiExampleResolver.Resolve(dimension, dimension, namingStrategy));
        }

        return this;
    }
}