// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Models;

/// <summary>
/// Represents the statistical neighbours response for a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record StatisticalNeighboursResponse
{
    /// <summary>
    /// The three-digit code of the local authority.
    /// </summary>
    public string? Code { get; init; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The array of statistical neighbours for this local authority.
    /// </summary>
    public StatisticalNeighbourResponse[]? StatisticalNeighbours { get; init; }
}

/// <summary>
/// Represents a single statistical neighbour for a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record StatisticalNeighbourResponse
{
    /// <summary>
    /// The three-digit code of the statistical neighbour local authority.
    /// </summary>
    public string? Code { get; init; }

    /// <summary>
    /// The name of the statistical neighbour local authority.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    /// The ranked position of the statistical neighbour (usually from 1 to 10).
    /// </summary>
    public int? Position { get; init; }
}