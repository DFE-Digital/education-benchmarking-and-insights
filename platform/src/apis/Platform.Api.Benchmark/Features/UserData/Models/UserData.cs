using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Benchmark.Features.UserData.Models;

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