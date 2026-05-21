using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Trust.Features.Accounts;

[ExcludeFromCodeCoverage]
[GenerateOpenApiExample(Name = "DimensionFinance", SourceType = typeof(Dimensions.Finance), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "CategoryCost", SourceType = typeof(Categories.Cost), SourceProperty = "All")]
public partial class OpenApiExamples;
