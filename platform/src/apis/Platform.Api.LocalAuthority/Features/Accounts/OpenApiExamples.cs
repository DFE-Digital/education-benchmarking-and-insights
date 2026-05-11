using System.Diagnostics.CodeAnalysis;
using Platform.Api.LocalAuthority.Features.Accounts.Validators;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.Accounts;

[ExcludeFromCodeCoverage]
[GenerateOpenApiExample(Name = "DimensionHighNeeds", SourceType = typeof(HighNeedsValidatorV2Values), SourceProperty = "Dimensions")]
[GenerateOpenApiExample(Name = "TypeHighNeeds", SourceType = typeof(HighNeedsValidatorV2Values), SourceProperty = "Types")]
public partial class OpenApiExamples;