// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record LocalAuthority<T> : LocalAuthorityBase
{
    public T? Outturn { get; set; }
    public T? Budget { get; set; }
}

public record LocalAuthorityBase
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double? Population2To18 { get; set; }
    public decimal? CarriedForwardBalance { get; set; }
}