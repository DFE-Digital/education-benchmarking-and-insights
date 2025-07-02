// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.Cache;

public record CacheOptions
{
    public CacheSettings ReturnYears { get; set; } = new();
    public CacheSettings CommercialResources { get; set; } = new();
    public CacheSettings Banners { get; set; } = new();
}

public record CacheSettings
{
    public int? SlidingExpiration { get; set; }
    public int? AbsoluteExpiration { get; set; }

    /// <summary>
    ///     If set, ignores sliding/absolute values to disable management of cache items
    /// </summary>
    public bool? Disabled { get; set; }
}