using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Insight.OpenApi;

[GenerateOpenApiExample(Name = "CategoryCensus", SourceType = typeof(Categories.Census), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "CategoryCost", SourceType = typeof(Categories.Cost), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "DimensionCensus", SourceType = typeof(Dimensions.Census), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "DimensionFinance", SourceType = typeof(Dimensions.Finance), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "DimensionSchoolsSummaryWorkforce", SourceType = typeof(Dimensions.SchoolsSummaryWorkforce), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "OverallPhase", SourceType = typeof(OverallPhase), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "FinanceTypes", SourceType = typeof(FinanceType), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "RagStatuses", SourceType = typeof(RagRating), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "CostCategories", SourceType = typeof(CostCategories), SourceProperty = "All")]
[GenerateOpenApiStringValuesExample(Name = "BudgetForecastRunType", Labels = new[] { "Default" }, Values = new[] { "default" })]
[GenerateOpenApiStringValuesExample(Name = "BudgetForecastRunCategory", Labels = new[] { "Revenue Reserve" }, Values = new[] { "Revenue reserve" })]
[GenerateOpenApiStringValuesExample(Name = "BudgetForecastRunId", Labels = new[] { "2022", "2023" }, Values = new[] { "2022", "2023" })]
public partial class OpenApiExamples
{
}