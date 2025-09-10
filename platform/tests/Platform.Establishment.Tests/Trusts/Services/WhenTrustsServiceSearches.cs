using Moq;
using Moq.Protected;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Search;
using Platform.Sql;
using Xunit;

namespace Platform.Establishment.Tests.Trusts.Services;

public class WhenTrustsServiceSearches
{
    private readonly Mock<IIndexClient> _client = new();
    private readonly Mock<IDatabaseFactory> _db = new();
    private readonly Mock<TrustsService> _service;

    public WhenTrustsServiceSearches()
    {
        _service = new Mock<TrustsService>(_client.Object, _db.Object)
        {
            CallBase = true
        };
    }

    [Fact]
    public async Task TrustSearchesAsyncShouldNotPassFacets()
    {
        string[]? capturedFacets = null;

        _service.Protected()
            .Setup<Task<SearchResponse<TrustSummary>>>(
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
            .ReturnsAsync(Mock.Of<SearchResponse<TrustSummary>>());

        var request = new SearchRequest();

        await _service.Object.TrustsSearchAsync(request);

        Assert.Null(capturedFacets);
    }

    [Fact]
    public async Task TrustSearchesAsyncShouldCorrectlyPassSearchText()
    {
        SearchRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SearchResponse<TrustSummary>>>(
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
            .ReturnsAsync(Mock.Of<SearchResponse<TrustSummary>>());

        var request = new SearchRequest
        {
            SearchText = nameof(SearchRequest.SearchText)
        };

        await _service.Object.TrustsSearchAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }

    [Fact]
    public async Task TrustsSuggestAsyncShouldCorrectlyPassFields()
    {
        string[]? capturedFields = null;

        var expected = new[]
        {
            nameof(TrustSummary.TrustName),
            nameof(TrustSummary.CompanyNumber)
        };

        _service.Protected()
            .Setup<Task<SuggestResponse<TrustSummary>>>(
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
            .ReturnsAsync(Mock.Of<SuggestResponse<TrustSummary>>());

        var request = new TrustSuggestRequest();

        await _service.Object.TrustsSuggestAsync(request);

        Assert.NotNull(capturedFields);
        Assert.Equivalent(expected, capturedFields);
    }

    [Fact]
    public async Task TrustsSuggestAsyncShouldCorrectlyPassSearchText()
    {
        SuggestRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SuggestResponse<TrustSummary>>>(
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
            .ReturnsAsync(Mock.Of<SuggestResponse<TrustSummary>>());

        var request = new TrustSuggestRequest
        {
            SearchText = nameof(TrustSuggestRequest.SearchText)
        };

        await _service.Object.TrustsSuggestAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }
}