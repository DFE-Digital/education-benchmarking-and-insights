using Web.App.Infrastructure.Apis;
using Web.App.ViewModels.Search;
using Xunit;

namespace Web.Tests.ViewModels.Search;

public class GivenASearchResultFacetViewModel
{
    [Fact]
    public void WhenCreateShouldMapFacetsIfSupplied()
    {
        var facets = new Dictionary<string, IList<FacetValueResponseModel>>
        {
            {
                "facet1", new List<FacetValueResponseModel>
                {
                    new()
                    {
                        Value = "facetValue1",
                        Count = 1
                    },
                    new()
                    {
                        Value = "facetValue2",
                        Count = 2
                    }
                }
            },
            {
                "facet2", new List<FacetValueResponseModel>
                {
                    new()
                    {
                        Value = "facetValue3",
                        Count = 3
                    }
                }
            }
        };

        var expected = new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>
        {
            {
                "Facet1", new List<SearchResultFacetViewModel>
                {
                    new()
                    {
                        Value = "facetValue1",
                        Count = 1
                    },
                    new()
                    {
                        Value = "facetValue2",
                        Count = 2
                    }
                }
            },
            {
                "Facet2", new List<SearchResultFacetViewModel>
                {
                    new()
                    {
                        Value = "facetValue3",
                        Count = 3
                    }
                }
            }
        };

        var actual = SearchResultFacetViewModel.Create(facets);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void WhenCreateShouldNotMapFacetsIfNotSupplied()
    {
        var expected = new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>();

        var actual = SearchResultFacetViewModel.Create(null);

        Assert.Equal(expected, actual);
    }
}