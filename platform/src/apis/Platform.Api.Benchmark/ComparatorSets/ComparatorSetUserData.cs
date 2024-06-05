using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record ComparatorSetUserData
{
    [ExplicitKey] public string? Id { get; set; }
    public string? UserId { get; set; }
    public string Type => "school-comparator-set";
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; } = "pending";
}