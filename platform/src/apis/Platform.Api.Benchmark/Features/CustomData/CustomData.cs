using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.CustomData;

[ExcludeFromCodeCoverage]
[Table("CustomDataSchool")]
public record CustomDataSchool
{
    [ExplicitKey] public string? Id { get; set; }
    [ExplicitKey] public string? URN { get; set; }
    public string? Data { get; set; }
}