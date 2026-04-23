using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Accounts;

[ExcludeFromCodeCoverage]
[GenerateOpenApiExample(Name = "Dimension", SourceType = typeof(Dimensions.Finance), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "Category", SourceType = typeof(Categories.Cost), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "Phase", SourceType = typeof(OverallPhase), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "FinanceTypes", SourceType = typeof(FinanceType), SourceProperty = "All")]
public partial class OpenApiExamples;
