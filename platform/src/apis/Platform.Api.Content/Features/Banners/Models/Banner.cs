using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Content.Features.Banners.Models;

[ExcludeFromCodeCoverage]
public record Banner
{
    public string? Title { get; set; }
    public string? Body { get; set; }
}