// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record HighNeedsYear : HighNeeds
{
    public string? Code { get; set; }
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record HighNeedsYearBase
{
    public string? Code { get; set; }
    public string? RunId { get; set; }
}