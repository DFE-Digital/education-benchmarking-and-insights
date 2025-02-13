using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.UserData;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record UserData
{
    [ExplicitKey] public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? Type { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; }
}