using System.Diagnostics.CodeAnalysis;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.Insight.Features.Census.Models;

[ExcludeFromCodeCoverage]
public record CensusHistoryModel : CensusModel
{
    public int? RunId { get; set; }
}