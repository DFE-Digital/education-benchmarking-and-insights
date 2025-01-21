using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Platform.Api.Insight.Features.Files.Models;
using Platform.Api.Insight.Features.Files.Responses;

namespace Platform.Api.Insight.Features.Files;

[ExcludeFromCodeCoverage]
public static class Mapper
{
    public static IEnumerable<FileResponse> MapToApiResponse(this IEnumerable<FileModel> models) => models.Select(MapToApiResponse);

    private static FileResponse MapToApiResponse(this FileModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        return new FileResponse
        {
            Type = model.Type,
            Label = model.Label,
            FileName = model.FileName
        };
    }
}