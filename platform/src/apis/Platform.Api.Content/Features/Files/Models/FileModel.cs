using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.Content.Features.Files.Models;

[ExcludeFromCodeCoverage]
public record FileModel
{
    public static readonly string[] Fields =
    [
        nameof(Type),
        nameof(Label),
        nameof(FileName),
        nameof(RunId),
        nameof(FileSize)
    ];

    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? FileName { get; set; }
    public string? RunId { get; init; }
    public decimal? FileSize { get; init; }
}
