// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain.LocalAuthorities;

public record LocalAuthority<T>
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double? Population2To18 { get; set; }
    public T? Outturn { get; set; }
    public T? Budget { get; set; }
}