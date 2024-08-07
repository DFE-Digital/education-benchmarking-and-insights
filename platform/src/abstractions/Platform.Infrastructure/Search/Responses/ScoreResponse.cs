using System.Diagnostics.CodeAnalysis;
namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public record ScoreResponse<T>
{
    public double? Score { get; set; }
    public T? Document { get; set; }
}