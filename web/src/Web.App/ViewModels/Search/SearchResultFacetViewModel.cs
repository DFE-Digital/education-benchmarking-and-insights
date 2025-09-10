using Web.App.Infrastructure.Apis;

// ReSharper disable MemberCanBePrivate.Global

namespace Web.App.ViewModels.Search;

public record SearchResultFacetViewModel
{
    public string? Value { get; init; }
    public long? Count { get; init; }

    public static Dictionary<string, IEnumerable<SearchResultFacetViewModel>> Create(Dictionary<string, IList<FacetValueResponseModel>>? facets)
    {
        if (facets == null)
        {
            return new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>();
        }

        return facets.Keys
            .ToDictionary(
                key => $"{key[..1].ToUpper()}{key[1..]}",
                key => facets[key]
                    .Select(x => new SearchResultFacetViewModel
                    {
                        Value = x.Value,
                        Count = x.Count
                    }));
    }
}