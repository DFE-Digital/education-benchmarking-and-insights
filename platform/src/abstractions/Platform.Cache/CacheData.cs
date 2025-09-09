// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Platform.Cache;

// this is a lightweight wrapper around the cached data to ensure that there is always a
// root JSON field, which is required when deserializing collections in Json.Net from BSON
public record CacheData<T>(T Data)
{
    public T Data { get; set; } = Data;
}