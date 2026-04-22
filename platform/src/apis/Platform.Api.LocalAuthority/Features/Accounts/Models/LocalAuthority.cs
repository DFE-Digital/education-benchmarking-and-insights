// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents local authority financial data.
/// </summary>
/// <typeparam name="T">The type of the financial data items.</typeparam>
[ExcludeFromCodeCoverage]
public record LocalAuthority<T> : LocalAuthorityBase
{
    /// <summary>
    /// The outturn financial data.
    /// </summary>
    public T? Outturn { get; set; }

    /// <summary>
    /// The budget financial data.
    /// </summary>
    public T? Budget { get; set; }
}

/// <summary>
/// The base record for local authority data.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthorityBase
{
    /// <summary>
    /// The three-digit local authority code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The population aged 2 to 18 in the local authority.
    /// </summary>
    public double? Population2To18 { get; set; }

    /// <summary>
    /// The total number of pupils in the local authority.
    /// </summary>
    public decimal? TotalPupils { get; set; }
}