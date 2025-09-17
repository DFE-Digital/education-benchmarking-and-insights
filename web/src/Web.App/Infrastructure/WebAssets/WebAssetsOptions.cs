using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.Infrastructure.WebAssets;

[ExcludeFromCodeCoverage]
public record WebAssetsOptions
{
    public string? FilesBaseUrl { get; set; }
    public string? ImagesBaseUrl { get; set; }
}