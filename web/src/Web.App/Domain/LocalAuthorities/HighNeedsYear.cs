// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain.LocalAuthorities;

public record HighNeedsYear : HighNeeds
{
    public string? Code { get; set; }
    public int? Year { get; set; }
}