using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Moq;
using Platform.Search;
using Platform.Search.Telemetry;
using Xunit;
namespace Platform.Tests.Search;

public class WhenSearchConnectionReceivesSearchByString
{
    private readonly Mock<SearchClient> _searchClient = new();
    private readonly SearchConnection<TestType> _service;
    private readonly Mock<ITelemetryService> _telemetryService = new();
    private string _indexName = nameof(_indexName);
    private string _serviceName = nameof(_serviceName);

    public WhenSearchConnectionReceivesSearchByString()
    {
        _searchClient.SetupGet(c => c.ServiceName).Returns(_serviceName);
        _searchClient.SetupGet(c => c.IndexName).Returns(_indexName);

        _service = new SearchConnection<TestType>(_searchClient.Object, _telemetryService.Object);
    }

    [Fact]
    public async Task ShouldReturnExpectedResults()
    {
        // arrange
        const string search = nameof(search);
        const string filters = nameof(filters);
        const int size = 10;
        const long totalCount = 5;
        const double resultScore = 1.0;

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
                    && o.IncludeTotalCount == true
                    && o.Filter == filters
                    && o.QueryType == SearchQueryType.Full), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        // act
        var (total, results) = await _service.SearchAsync(search, filters, size);

        // assert
        Assert.Equal(total, totalCount);
        var scoreResponses = results as ScoreResponse<TestType>[] ?? results.ToArray();
        Assert.Equal([result], scoreResponses.Select(r => r.Document));
        Assert.Equal([resultScore], scoreResponses.Select(r => r.Score));
    }

    [Fact]
    public async Task ShouldTrackSearchEvent()
    {
        // arrange
        const string search = nameof(search);
        const string filters = nameof(filters);
        const int size = 10;
        const long totalCount = 5;

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
        await _service.SearchAsync(search, filters, size);

        // assert
        _telemetryService.Verify(t => t.TrackSearchEvent(It.Is<SearchTelemetryProperties>(p =>
            p.SearchId != Guid.Empty
            && p.SearchServiceName == _serviceName
            && p.IndexName == _indexName
            && p.Filter == filters
            && p.SearchText == search
            && p.ResultCount == totalCount)));
    }

    private record TestType;
}