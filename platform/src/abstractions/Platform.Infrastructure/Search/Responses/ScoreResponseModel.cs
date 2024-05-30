namespace Platform.Infrastructure.Search;

public record ScoreResponseModel<T>
{
    public double? Score { get; set; }
    public T? Document { get; set; }
}