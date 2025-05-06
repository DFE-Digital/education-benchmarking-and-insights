using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Api.Benchmark.Features.ComparatorSets.Requests;

[ExcludeFromCodeCoverage]
public record ComparatorSetUserDefinedRequest
{
    public string? UserId { get; set; }
    public string[] Set { get; set; } = [];
}