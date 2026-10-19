using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Content.Features.Files.Models;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Content.Features.Files.Responses;

[ExcludeFromCodeCoverage]
public record FileResponse
{
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? FileName { get; set; }
    public string? RunId { get; init; }
    public decimal? FileSize { get; init; }
}

public static class FileResponseExtensions
{
    public static FileResponse MapToApiResponse(this FileModel model) => new()
    {
        Type = model.Type,
        Label = model.Label,
        FileName = model.FileName,
        RunId = model.RunId,
        FileSize = model.FileSize
    };

    public static IEnumerable<FileResponse> MapToApiResponse(this IEnumerable<FileModel> models) =>
        models.Select(MapToApiResponse);
}
