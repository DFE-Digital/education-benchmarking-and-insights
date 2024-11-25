using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Moq;
using Platform.Search;
using Platform.Search.Telemetry;
using Xunit;
namespace Platform.Tests.Search;

public class WhenSearchConnectionReceivesSearchBySearchRequest
{
    private readonly Mock<SearchClient> _searchClient = new();
    private readonly SearchConnection<TestType> _service;
    private readonly Mock<ITelemetryService> _telemetryService = new();
    private string _indexName = nameof(_indexName);
    private string _serviceName = nameof(_serviceName);

    public WhenSearchConnectionReceivesSearchBySearchRequest()
    {
        _searchClient.SetupGet(c => c.ServiceName).Returns(_serviceName);
        _searchClient.SetupGet(c => c.IndexName).Returns(_indexName);

        _service = new SearchConnection<TestType>(_searchClient.Object, _telemetryService.Object);
    }

    [Fact]
    public async Task ShouldReturnExpectedResultsWithoutFacets()
    {
        // arrange
        const string search = nameof(search);
        FilterCriteria[] filters =
        [
            new()
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            }
        ];
        const int size = 10;
        const int page = 1;
        const long totalCount = 5;
        const double resultScore = 1.0;

        var request = new SearchRequest
        {
            Filters = filters,
            PageSize = size,
            Page = page,
            SearchText = search
        };

        Func<FilterCriteria[], string?> filterBuilder = _ => nameof(filterBuilder);

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [
                SearchModelFactory.SearchResult(result, resultScore, new Dictionary<string, IList<string>>())
            ],
            totalCount,
            new Dictionary<string, IList<FacetResult>>(),
            null,
            Mock.Of<Response>());

        _searchClient
            .Setup(c => c.SearchAsync<TestType>(
                search,
                It.Is<SearchOptions>(o =>
                    o.Size == size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == nameof(filterBuilder)
                    && o.QueryType == SearchQueryType.Simple), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        // act
        var results = await _service.SearchAsync(request, filterBuilder);

        // assert
        Assert.Equal(totalCount, results.TotalResults);
        Assert.Equal([result], results.Results);
    }

    [Fact]
    public async Task ShouldReturnExpectedResultsWithFacets()
    {
        // arrange
        const string search = nameof(search);
        FilterCriteria[] filters =
        [
            new()
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            }
        ];
        const int size = 10;
        const int page = 1;
        const long totalCount = 5;
        const double resultScore = 1.0;
        string[] facets = [nameof(facets)];

        var request = new SearchRequest
        {
            Filters = filters,
            PageSize = size,
            Page = page,
            SearchText = search
        };

        Func<FilterCriteria[], string?> filterBuilder = _ => nameof(filterBuilder);

        const int facetCount = 3;
        const string facetResultKey = nameof(facetResultKey);
        const string facetValue = nameof(facetValue);
        var facetResult = SearchModelFactory.FacetResult(facetCount, new Dictionary<string, object>
        {
            {
                "value", facetValue
            }
        });
        var resultFacets = new Dictionary<string, IList<FacetResult>>
        {
            {
                facetResultKey, new List<FacetResult>
                {
                    facetResult
                }
            }
        };

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [
                SearchModelFactory.SearchResult(result, resultScore, new Dictionary<string, IList<string>>())
            ],
            totalCount,
            resultFacets,
            null,
            Mock.Of<Response>());

        _searchClient
            .Setup(c => c.SearchAsync<TestType>(
                search,
                It.Is<SearchOptions>(o =>
                    o.Size == size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == nameof(filterBuilder)
                    && o.QueryType == SearchQueryType.Simple
                    && o.Facets[0] == facets[0]), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        var expectedFacets = new Dictionary<string, IList<FacetValueResponseModel>>
        {
            {
                facetResultKey, new List<FacetValueResponseModel>
                {
                    new()
                    {
                        Value = facetValue,
                        Count = facetCount
                    }
                }
            }
        };

        // act
        var results = await _service.SearchAsync(request, filterBuilder, facets);

        // assert
        Assert.Equal(totalCount, results.TotalResults);
        Assert.Equal([result], results.Results);
        Assert.Equal(expectedFacets, results.Facets);
    }

    [Fact]
    public async Task ShouldTrackSearchEvent()
    {
        // arrange
        const string search = nameof(search);
        FilterCriteria[] filters =
        [
            new()
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            }
        ];
        const int size = 10;
        const int page = 1;
        const long totalCount = 5;

        var request = new SearchRequest
        {
            Filters = filters,
            PageSize = size,
            Page = page,
            SearchText = search
        };

        Func<FilterCriteria[], string?> filterBuilder = _ => nameof(filterBuilder);

        var searchResults = SearchModelFactory.SearchResults(
            Array.Empty<SearchResult<TestType>>(),
            totalCount,
            new Dictionary<string, IList<FacetResult>>(),
            null,
            Mock.Of<Response>());

        _searchClient
            .Setup(c => c.SearchAsync<TestType>(It.IsAny<string>(), It.IsAny<SearchOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        // act
        await _service.SearchAsync(request, filterBuilder);

        // assert
        _telemetryService.Verify(t => t.TrackSearchEvent(It.Is<SearchTelemetryProperties>(p =>
            p.SearchId != Guid.Empty
            && p.SearchServiceName == _serviceName
            && p.IndexName == _indexName
            && p.Filter == nameof(filterBuilder)
            && p.SearchText == search
            && p.ResultCount == totalCount)));
    }

    private record TestType;
}