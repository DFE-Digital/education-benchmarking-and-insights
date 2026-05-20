using System.Diagnostics.CodeAnalysis;
using Platform.Domain;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Details;

[ExcludeFromCodeCoverage]
[GenerateOpenApiExample(Name = "DimensionFinance", SourceType = typeof(Dimensions.Finance), SourceProperty = "All")]
[GenerateOpenApiExample(Name = "DimensionWorkforce", SourceType = typeof(Dimensions.SchoolsSummaryWorkforce), SourceProperty = "All")]
public partial class OpenApiExamples;
