// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record HighNeedsYear : HighNeeds
{
    public string? Code { get; set; }
    public int? Year { get; set; }
}