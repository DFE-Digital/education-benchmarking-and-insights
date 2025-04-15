using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels.Search;
using Xunit;

namespace Web.Tests.ViewModels.Search;

public class GivenASchoolSearchResultsViewModel
{
    private static readonly Fixture Fixture = new();
    private static readonly Random Random = new();
    private static readonly SearchResultFacetViewModel[] OverallPhaseFacets =
    [
        new()
        {
            Value = OverallPhaseTypes.Secondary,
            Count = 3
        },
        new()
        {
            Value = OverallPhaseTypes.Special,
            Count = 2
        },
        new()
        {
            Value = OverallPhaseTypes.AllThrough,
            Count = 1
        }
    ];

    public static TheoryData<Dictionary<string, IEnumerable<SearchResultFacetViewModel>>, string[], SearchResultFacetViewModel[]> FilterAndMergeOverallPhaseFacetsData = new()
    {
        {
            new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>
            {
                {
                    "facet", new List<SearchResultFacetViewModel>()
                }
            },
            [],
            []
        },
        {
            new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>
            {
                {
                    "facet", new List<SearchResultFacetViewModel>()
                },
                {
                    "OverallPhase", OverallPhaseFacets
                }
            },
            [],
            OverallPhaseFacets
        },
        {
            new Dictionary<string, IEnumerable<SearchResultFacetViewModel>>
            {
                {
                    "facet", new List<SearchResultFacetViewModel>()
                },
                {
                    "OverallPhase", OverallPhaseFacets
                }
            },
            ["Unsupported Phase", OverallPhaseTypes.Primary, OverallPhaseTypes.Nursery, OverallPhaseTypes.Secondary],
            OverallPhaseFacets.Concat(
            [
                new SearchResultFacetViewModel { Value = OverallPhaseTypes.Nursery, Count = 0 },
                new SearchResultFacetViewModel { Value = OverallPhaseTypes.Primary, Count = 0 }
            ]).ToArray()
        }
    };

    [Theory]
    [MemberData(nameof(FilterAndMergeOverallPhaseFacetsData))]
    public void ShouldFilterAndMergeOverallPhaseFacets(Dictionary<string, IEnumerable<SearchResultFacetViewModel>> facets, string[] all, SearchResultFacetViewModel[] expected)
    {
        var viewModel = Fixture
            .Build<SchoolSearchResultsViewModel>()
            .With(s => s.Facets, facets)
            .With(s => s.OverallPhaseAll, all)
            .Create();

        Assert.Equal(expected, viewModel.OverallPhaseFacets);
    }
}