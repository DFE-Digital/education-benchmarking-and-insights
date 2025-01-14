using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Moq;
using Platform.Search.Requests;
using Platform.Search.Telemetry;
using Xunit;

namespace Platform.Search.Tests;

public class WhenSearchConnectionReceivesSuggestRequest
{
    private readonly Mock<SearchClient> _searchClient = new();
    private readonly SearchConnection<TestType> _service;
    private readonly Mock<ITelemetryService> _telemetryService = new();
    private string _indexName = nameof(_indexName);
    private string _serviceName = nameof(_serviceName);

    public WhenSearchConnectionReceivesSuggestRequest()
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
        const string suggesterName = nameof(suggesterName);
        const int size = 10;
        const double coverage = 1.0;

        var request = new SuggestRequest
        {
            SuggesterName = suggesterName,
            Size = size,
            SearchText = search
        };

        Func<string?> filterBuilder = () => nameof(filterBuilder);
        string[] selectFields = [nameof(selectFields)];

        var result = new TestType();
        var suggestion = SearchModelFactory.SearchSuggestion(result, search);
        var suggestResults = SearchModelFactory.SuggestResults<TestType>([suggestion], coverage);

        _searchClient
            .Setup(c => c.SuggestAsync<TestType>(
                search,
                suggesterName,
                It.Is<SuggestOptions>(o =>
                    o.HighlightPreTag == "*"
                    && o.HighlightPostTag == "*"
                    && o.Filter == nameof(filterBuilder)
                    && o.Size == size
                    && o.UseFuzzyMatching == false
                    && o.Select[0] == selectFields[0]), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(suggestResults, Mock.Of<Response>()));

        // act
        var results = await _service.SuggestAsync(request, filterBuilder, selectFields);

        // assert
        Assert.Equal([result], results.Results.Select(r => r.Document));
    }

    [Fact]
    public async Task ShouldTrackSuggestEvent()
    {
        // arrange
        const string search = nameof(search);
        const string suggesterName = nameof(suggesterName);
        const int size = 10;
        const int totalCount = 5;
        const double coverage = 1.0;

        var request = new SuggestRequest
        {
            SuggesterName = suggesterName,
            Size = size,
            SearchText = search
        };

        Func<string?> filterBuilder = () => nameof(filterBuilder);
        string[] selectFields = [nameof(selectFields)];

        var suggestResults = SearchModelFactory.SuggestResults(new SearchSuggestion<TestType>[totalCount], coverage);

        _searchClient
            .Setup(c => c.SuggestAsync<TestType>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SuggestOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(suggestResults, Mock.Of<Response>()));

        // act
        await _service.SuggestAsync(request, filterBuilder, selectFields);

        // assert
        _telemetryService.Verify(t => t.TrackSuggestEvent(It.Is<SuggestTelemetryProperties>(p =>
            p.SearchId != Guid.Empty
            && p.SearchServiceName == _serviceName
            && p.IndexName == _indexName
            && p.SuggesterName == suggesterName
            && p.Fields[0] == nameof(selectFields)
            && p.Filter == nameof(filterBuilder)
            && p.SearchText == search
            && p.ResultCount == totalCount)));
    }

    private record TestType;
}