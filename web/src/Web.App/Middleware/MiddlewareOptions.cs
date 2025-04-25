// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Web.App.Middleware;

public record MiddlewareOptions
{
    public string? CanonicalHostName { get; set; }
}