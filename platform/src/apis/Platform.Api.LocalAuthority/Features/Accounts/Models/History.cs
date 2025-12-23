// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

public record History<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? Outturn { get; set; } = [];
    public T[]? Budget { get; set; } = [];
}