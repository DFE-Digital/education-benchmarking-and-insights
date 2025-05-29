namespace Web.App.Cache;

public record CacheOptions
{
    public CacheSettings ReturnYears { get; set; } = new();
    public CacheSettings CommercialResources { get; set; } = new();
}

public record CacheSettings
{
    public int? SlidingExpiration { get; set; }
    public int? AbsoluteExpiration { get; set; }
}