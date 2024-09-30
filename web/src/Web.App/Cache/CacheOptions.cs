namespace Web.App.Cache;

public class CacheOptions
{
    public string? CacheKey { get; set; }
    public int SlidingExpirationInSeconds { get; set; }
    public int AbsoluteExpirationInSeconds { get; set; }
}