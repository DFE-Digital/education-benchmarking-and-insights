// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthority<T> : LocalAuthorityBase
{
    public T? Outturn { get; set; }
    public T? Budget { get; set; }
}

[ExcludeFromCodeCoverage]
public record LocalAuthorityBase
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double? Population2To18 { get; set; }
    public decimal? TotalPupils { get; set; }
}