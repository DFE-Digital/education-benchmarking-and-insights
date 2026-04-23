using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.MetricRagRatings;

[ExcludeFromCodeCoverage]
[GenerateOpenApiExample(Name = "CategoryCost", SourceType = typeof(Categories.Cost), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "RagStatuses", SourceType = typeof(RagRating), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "Phase", SourceType = typeof(OverallPhase), SourceProperty = "All")]
public partial class OpenApiExamples;