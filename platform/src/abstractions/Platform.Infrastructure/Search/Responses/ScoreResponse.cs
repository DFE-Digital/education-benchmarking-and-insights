namespace Platform.Infrastructure.Search;

public record ScoreResponse<T>
{
    public double? Score { get; set; }
    public T? Document { get; set; }
}