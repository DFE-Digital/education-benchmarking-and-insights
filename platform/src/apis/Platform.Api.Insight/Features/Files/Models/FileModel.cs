using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.Insight.Features.Files.Models;

[ExcludeFromCodeCoverage]
public record FileModel
{
    public static readonly string[] Fields = [nameof(Type), nameof(Label), nameof(FileName)];
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? FileName { get; set; }
}