using System.Diagnostics.CodeAnalysis;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Platform.Search;

[ExcludeFromCodeCoverage]
public record ScoreResponse<T>
{
    public double? Score { get; set; }
    public T? Document { get; set; }
}