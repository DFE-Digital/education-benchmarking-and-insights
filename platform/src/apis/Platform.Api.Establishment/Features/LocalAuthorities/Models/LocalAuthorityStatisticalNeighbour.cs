using System.Diagnostics.CodeAnalysis;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthorityStatisticalNeighbour
{
    public string? RunId { get; set; }
    public string? LaCode { get; set; }
    public string? LaName { get; set; }
    public byte? NeighbourPosition { get; set; }
    public string? NeighbourLaCode { get; set; }
    public string? NeighbourLaName { get; set; }
}