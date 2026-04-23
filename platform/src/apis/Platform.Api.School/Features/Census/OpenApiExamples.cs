using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Census;

[GenerateOpenApiExample(Name = "Dimension", SourceType = typeof(Dimensions.Census), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "Category", SourceType = typeof(Categories.Census), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "Phase", SourceType = typeof(OverallPhase), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "FinanceTypes", SourceType = typeof(FinanceType), SourceProperty = "All")]
[GenerateOpenApiPropertiesExample(Name = "SeniorLeadershipDimension", SourceType = typeof(Dimensions.Census), Properties = new[] { "Total", "PercentWorkforce" })]
public static partial class OpenApiExamples
{
}