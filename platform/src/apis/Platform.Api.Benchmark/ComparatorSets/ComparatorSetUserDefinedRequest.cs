using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
public record ComparatorSetUserDefinedRequest
{
    public string? UserId { get; set; }
    public string[] Set { get; set; } = Array.Empty<string>();
};