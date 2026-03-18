// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain.LocalAuthorities;

public record LocalAuthorityHighNeeds<T> : LocalAuthority
{
    public T? Outturn { get; set; }
    public T? Budget { get; set; }
}