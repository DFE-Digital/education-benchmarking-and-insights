using System.Diagnostics.CodeAnalysis;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Insight.Features.Files.Responses;

[ExcludeFromCodeCoverage]
public record FileResponse
{
    public string? Label { get; set; }
    public string? FileName { get; set; }
}