using Platform.OpenApi.Attributes;

namespace Platform.Api.Benchmark.OpenApi;

[GenerateOpenApiIntValuesExample(Name = "ExampleYear", Labels = new[] { "2022", "2023" }, Values = new[] { 2022, 2023 })]
[GenerateOpenApiStringValuesExample(Name = "UserDataType", Labels = new[] { "Comparator set", "Custom data" }, Values = new[] { "comparator-set", "custom-data" })]
[GenerateOpenApiStringValuesExample(Name = "OrganisationType", Labels = new[] { "School", "Trust" }, Values = new[] { "school", "trust" })]
[GenerateOpenApiStringValuesExample(Name = "UserDataStatus", Labels = new[] { "Pending", "Complete", "Failed" }, Values = new[] { "pending", "complete", "failed" })]
public partial class OpenApiExamples
{
}
