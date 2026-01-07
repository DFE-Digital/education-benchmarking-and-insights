// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record TopFunding
{
    public decimal? EarlyYears { get; set; }
    public decimal? Primary { get; set; }
    public decimal? Secondary { get; set; }
    public decimal? Special { get; set; }
    public decimal? AlternativeProvision { get; set; }
    public decimal? PostSchool { get; set; }
    public decimal? Income { get; set; }
}