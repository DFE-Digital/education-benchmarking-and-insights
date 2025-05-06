using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Moq;
using Xunit;

namespace Platform.Search.Tests;

public class SearchSearchTests
{
    private const string Search = nameof(Search);
    private const int Size = 10;
    private const int Page = 1;
    private const long TotalCount = 5;
    private const double ResultScore = 1.0;
    private const string SuggesterName = nameof(SuggesterName);
    private const double Coverage = 1.0;

    private readonly Mock<IIndexClient> _client = new();
    private readonly TestService _service;

    public SearchSearchTests()
    {
        _service = new TestService(_client.Object);
    }

    [Fact]
    public async Task LookUpShouldReturnExpectedResult()
    {
        const string key = nameof(key);
        var result = new TestType();

        _client
            .Setup(c => c.GetDocumentAsync<TestType>(key))
            .ReturnsAsync(Response.FromValue(result, Mock.Of<Response>()));

        var results = await _service.CallLookUpAsync(key);

        Assert.Equal(result, results);
    }

    [Fact]
    public async Task SearchShouldReturnExpectedResultsWithoutFacets()
    {

        FilterCriteria[] filters =
        [
            new()
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            }
        ];

        var request = new SearchRequest
        {
            Filters = filters,
            PageSize = Size,
            Page = Page,
            SearchText = Search
        };

        Func<string?> filterBuilder = () => nameof(filterBuilder);

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [SearchModelFactory.SearchResult(result, ResultScore, new Dictionary<string, IList<string>>())],
            TotalCount,
            new Dictionary<string, IList<FacetResult>>(),
            null,
            Mock.Of<Response>());

        _client
            .Setup(c => c.SearchAsync<TestType>(
                Search,
                It.Is<SearchOptions>(o =>
                    o.Size == Size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == nameof(filterBuilder)
                    && o.QueryType == SearchQueryType.Simple)))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));


        var results = await _service.CallSearchAsync(request, filterBuilder);

        Assert.Equal(TotalCount, results.TotalResults);
        Assert.Equal([result], results.Results);
    }

    [Fact]
    public async Task SearchShouldCorrectlyPassOrderByWithOrderBy()
    {
        SearchOptions? capturedOptions = null;

        var orderBy = new OrderByCriteria
        {
            Field = nameof(OrderByCriteria.Field),
            Value = nameof(OrderByCriteria.Value)
        };

        string[] expectedOrderBy = [$"{orderBy.Field} {orderBy.Value.ToLower()}"];

        var request = new SearchRequest
        {
            OrderBy = orderBy,
            PageSize = Size,
            Page = Page,
            SearchText = Search
        };

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [SearchModelFactory.SearchResult(result, ResultScore, new Dictionary<string, IList<string>>())],
            TotalCount,
            null,
            null,
            Mock.Of<Response>());

        _client
            .Setup(c => c.SearchAsync<TestType>(
                Search,
                It.Is<SearchOptions>(o =>
                    o.Size == Size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == null
                    && o.QueryType == SearchQueryType.Simple)))
            .Callback((string _, SearchOptions options) =>
            {
                capturedOptions = options;
            })
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        await _service.CallSearchAsync(request);

        Assert.NotNull(capturedOptions);
        Assert.NotEmpty(capturedOptions.OrderBy);
        Assert.Equal(expectedOrderBy, capturedOptions.OrderBy);
    }

    [Fact]
    public async Task SearchShouldCorrectlyNotPassOrderByWithoutOrderBy()
    {
        SearchOptions? capturedOptions = null;

        var request = new SearchRequest
        {
            PageSize = Size,
            Page = Page,
            SearchText = Search
        };

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [SearchModelFactory.SearchResult(result, ResultScore, new Dictionary<string, IList<string>>())],
            TotalCount,
            null,
            null,
            Mock.Of<Response>());

        _client
            .Setup(c => c.SearchAsync<TestType>(
                Search,
                It.Is<SearchOptions>(o =>
                    o.Size == Size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == null
                    && o.QueryType == SearchQueryType.Simple)))
            .Callback((string _, SearchOptions options) =>
            {
                capturedOptions = options;
            })
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        await _service.CallSearchAsync(request);

        Assert.NotNull(capturedOptions);
        Assert.Empty(capturedOptions.OrderBy);
    }

    [Fact]
    public async Task SearchShouldReturnExpectedResultsWithFacets()
    {
        FilterCriteria[] filters =
        [
            new()
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            }
        ];

        string[] facets = [nameof(facets)];

        var request = new SearchRequest
        {
            Filters = filters,
            PageSize = Size,
            Page = Page,
            SearchText = Search
        };

        Func<string?> filterBuilder = () => nameof(filterBuilder);

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
                SearchModelFactory.SearchResult(result, ResultScore, new Dictionary<string, IList<string>>())
            ],
            TotalCount,
            resultFacets,
            null,
            Mock.Of<Response>());

        _client
            .Setup(c => c.SearchAsync<TestType>(
                Search,
                It.Is<SearchOptions>(o =>
                    o.Size == Size
                    && o.Skip == 0
                    && o.IncludeTotalCount == true
                    && o.Filter == nameof(filterBuilder)
                    && o.QueryType == SearchQueryType.Simple
                    && o.Facets[0] == facets[0])))
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

        var results = await _service.CallSearchAsync(request, filterBuilder, facets);

        Assert.Equal(TotalCount, results.TotalResults);
        Assert.Equal([result], results.Results);
        Assert.Equal(expectedFacets, results.Facets);
    }


    [Fact]
    public async Task SearchWithScoreShouldReturnExpectedResults()
    {
        const string filters = nameof(filters);

        var result = new TestType();
        var searchResults = SearchModelFactory.SearchResults(
            [
                SearchModelFactory.SearchResult(result, ResultScore, new Dictionary<string, IList<string>>())
            ],
            TotalCount,
            new Dictionary<string, IList<FacetResult>>(),
            null,
            Mock.Of<Response>());

        _client
            .Setup(c => c.SearchAsync<TestType>(
                Search,
                It.Is<SearchOptions>(o =>
                    o.Size == Size
                    && o.IncludeTotalCount == true
                    && o.Filter == filters
                    && o.QueryType == SearchQueryType.Full)))
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        var (total, results) = await _service.CallSearchWithScoreAsync(Search, filters, Size);

        Assert.Equal(total, TotalCount);
        var scoreResponses = results as ScoreResponse<TestType>[] ?? results.ToArray();
        Assert.Equal([result], scoreResponses.Select(r => r.Document));
        Assert.Equal([ResultScore], scoreResponses.Select(r => r.Score));
    }

    [Fact]
    public async Task ShouldReturnExpectedResultsWithoutFacets()
    {
        var request = new TestSuggestRequest
        {
            Size = Size,
            SearchText = Search
        };

        Func<string?> filterBuilder = () => nameof(filterBuilder);
        string[] selectFields = [nameof(selectFields)];

        var result = new TestType();
        var suggestion = SearchModelFactory.SearchSuggestion(result, Search);
        var suggestResults = SearchModelFactory.SuggestResults<TestType>([suggestion], Coverage);

        _client
            .Setup(c => c.SuggestAsync<TestType>(
                Search,
                SuggesterName,
                It.Is<SuggestOptions>(o =>
                    o.HighlightPreTag == "*"
                    && o.HighlightPostTag == "*"
                    && o.Filter == nameof(filterBuilder)
                    && o.Size == Size
                    && o.UseFuzzyMatching == false
                    && o.Select[0] == selectFields[0]),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(suggestResults, Mock.Of<Response>()));

        var results = await _service.CallSuggestAsync(request, filterBuilder, selectFields);

        Assert.Equal([result], results.Results.Select(r => r.Document));
    }
}