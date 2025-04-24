using Web.App.Domain;

// ReSharper disable MemberCanBePrivate.Global

namespace Web.App.ViewModels.Search;

public record LocalAuthoritySearchResultViewModel
{
    public string? Code { get; init; }
    public string? Name { get; init; }

    public static LocalAuthoritySearchResultViewModel Create(LocalAuthoritySummary localAuthority)
    {
        return new LocalAuthoritySearchResultViewModel
        {
            Code = localAuthority.Code,
            Name = localAuthority.Name
        };
    }
}