using Web.App.Infrastructure.Apis;

// ReSharper disable MemberCanBePrivate.Global
namespace Web.App.ViewModels.Search;

public class SearchResultFacetViewModel : IEquatable<SearchResultFacetViewModel>
{
    public string? Value { get; init; }
    public long? Count { get; init; }

    public bool Equals(SearchResultFacetViewModel? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Value == other.Value;
    }

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
                    .Select(x => new SearchResultFacetViewModel { Value = x.Value, Count = x.Count }));
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((SearchResultFacetViewModel)obj);
    }

    public override int GetHashCode()
    {
        return Value != null ? Value.GetHashCode() : 0;
    }
}