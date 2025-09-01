using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Content.Features.News.Models;

[ExcludeFromCodeCoverage]
public record News
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Body { get; set; }
    public DateTime? Updated { get; set; }
}