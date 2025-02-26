// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Domain.LocalAuthorities;

public record History<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? Outturn { get; set; } = [];
    public T[]? Budget { get; set; } = [];
}