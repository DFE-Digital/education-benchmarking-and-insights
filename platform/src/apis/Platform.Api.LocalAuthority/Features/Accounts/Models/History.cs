// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents historical data over a range of years.
/// </summary>
/// <typeparam name="T">The type of the historical data items.</typeparam>
[ExcludeFromCodeCoverage]
public record History<T>
{
    /// <summary>
    /// The start year of the history range.
    /// </summary>
    public int? StartYear { get; set; }

    /// <summary>
    /// The end year of the history range.
    /// </summary>
    public int? EndYear { get; set; }

    /// <summary>
    /// The outturn historical data.
    /// </summary>
    public T[]? Outturn { get; set; } = [];

    /// <summary>
    /// The budget historical data.
    /// </summary>
    public T[]? Budget { get; set; } = [];
}