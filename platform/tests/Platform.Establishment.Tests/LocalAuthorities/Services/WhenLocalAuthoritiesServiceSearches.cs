using Moq;
using Moq.Protected;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Requests;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Search;
using Platform.Sql;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities.Services;

public class WhenLocalAuthoritiesServiceSearches
{
    private readonly Mock<IIndexClient> _client = new();
    private readonly Mock<IDatabaseFactory> _db = new();
    private readonly Mock<LocalAuthoritiesService> _service;

    public WhenLocalAuthoritiesServiceSearches()
    {
        _service = new Mock<LocalAuthoritiesService>(_client.Object, _db.Object)
        {
            CallBase = true
        };
    }

    [Fact]
    public async Task LocalAuthoritySearchesAsyncShouldNotPassFacets()
    {
        string[]? capturedFacets = null;

        _service.Protected()
            .Setup<Task<SearchResponse<LocalAuthoritySummary>>>(
                "SearchAsync",
                ItExpr.IsAny<SearchRequest>(),
                ItExpr.IsAny<Func<string?>>(),
                ItExpr.IsAny<string[]?>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback((SearchRequest _, Func<string?> _, string[] facets, CancellationToken _) =>
            {
                capturedFacets = facets;
            })
            .ReturnsAsync(Mock.Of<SearchResponse<LocalAuthoritySummary>>());

        var request = new SearchRequest();

        await _service.Object.LocalAuthoritiesSearchAsync(request);

        Assert.Null(capturedFacets);
    }

    [Fact]
    public async Task LocalAuthoritySearchesAsyncShouldCorrectlyPassSearchText()
    {
        SearchRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SearchResponse<LocalAuthoritySummary>>>(
                "SearchAsync",
                ItExpr.IsAny<SearchRequest>(),
                ItExpr.IsAny<Func<string?>>(),
                ItExpr.IsAny<string[]?>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback((SearchRequest request, Func<string?> _, string[] _, CancellationToken _) =>
            {
                capturedRequest = request;
            })
            .ReturnsAsync(Mock.Of<SearchResponse<LocalAuthoritySummary>>());

        var request = new SearchRequest
        {
            SearchText = nameof(SearchRequest.SearchText)
        };

        await _service.Object.LocalAuthoritiesSearchAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }

    [Fact]
    public async Task LocalAuthoritiesSuggestAsyncShouldCorrectlyPassFields()
    {
        string[]? capturedFields = null;

        var expected = new[] { nameof(LocalAuthoritySummary.Name), nameof(LocalAuthoritySummary.Code) };

        _service.Protected()
            .Setup<Task<SuggestResponse<LocalAuthoritySummary>>>(
                "SuggestAsync",
                ItExpr.IsAny<SuggestRequest>(),
                ItExpr.IsAny<Func<string?>?>(),
                ItExpr.IsAny<string[]?>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback((SuggestRequest _, Func<string?>? _, string[] fields, CancellationToken _) =>
            {
                capturedFields = fields;
            })
            .ReturnsAsync(Mock.Of<SuggestResponse<LocalAuthoritySummary>>());

        var request = new LocalAuthoritySuggestRequest();

        await _service.Object.LocalAuthoritiesSuggestAsync(request);

        Assert.NotNull(capturedFields);
        Assert.Equivalent(expected, capturedFields);
    }

    [Fact]
    public async Task LocalAuthoritiesSuggestAsyncShouldCorrectlyPassSearchText()
    {
        SuggestRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SuggestResponse<LocalAuthoritySummary>>>(
                "SuggestAsync",
                ItExpr.IsAny<SuggestRequest>(),
                ItExpr.IsAny<Func<string?>?>(),
                ItExpr.IsAny<string[]?>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Callback((SuggestRequest request, Func<string?>? _, string[] _, CancellationToken _) =>
            {
                capturedRequest = request;
            })
            .ReturnsAsync(Mock.Of<SuggestResponse<LocalAuthoritySummary>>());

        var request = new LocalAuthoritySuggestRequest
        {
            SearchText = nameof(LocalAuthoritySuggestRequest.SearchText)
        };

        await _service.Object.LocalAuthoritiesSuggestAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }
}