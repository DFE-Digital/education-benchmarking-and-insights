using Moq;
using Moq.Protected;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Search;
using Platform.Sql;
using Xunit;

namespace Platform.Establishment.Tests.Schools.Services;

public class WhenSchoolsServiceSearches
{
    private readonly Mock<IIndexClient> _client = new();
    private readonly Mock<IDatabaseFactory> _db = new();
    private readonly Mock<SchoolsService> _service;

    public WhenSchoolsServiceSearches()
    {
        _service = new Mock<SchoolsService>(_client.Object, _db.Object)
        {
            CallBase = true
        };
    }

    [Fact]
    public async Task SchoolSearchesAsyncShouldNotPassFacets()
    {
        string[]? capturedFacets = null;

        _service.Protected()
            .Setup<Task<SearchResponse<SchoolSummary>>>(
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
            .ReturnsAsync(Mock.Of<SearchResponse<SchoolSummary>>());

        var request = new SearchRequest();

        await _service.Object.SchoolsSearchAsync(request);

        Assert.Null(capturedFacets);
    }

    [Fact]
    public async Task SchoolSearchesAsyncShouldCorrectlyPassSearchText()
    {
        SearchRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SearchResponse<SchoolSummary>>>(
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
            .ReturnsAsync(Mock.Of<SearchResponse<SchoolSummary>>());

        var request = new SearchRequest
        {
            SearchText = nameof(SearchRequest.SearchText)
        };

        await _service.Object.SchoolsSearchAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }

    [Fact]
    public async Task SchoolsSuggestAsyncShouldCorrectlyPassFields()
    {
        string[]? capturedFields = null;

        var expected = new[]
        {
            nameof(SchoolSummary.SchoolName),
            nameof(SchoolSummary.URN),
            nameof(SchoolSummary.AddressStreet),
            nameof(SchoolSummary.AddressLocality),
            nameof(SchoolSummary.AddressLine3),
            nameof(SchoolSummary.AddressTown),
            nameof(SchoolSummary.AddressCounty),
            nameof(SchoolSummary.AddressPostcode)
        };

        _service.Protected()
            .Setup<Task<SuggestResponse<SchoolSummary>>>(
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
            .ReturnsAsync(Mock.Of<SuggestResponse<SchoolSummary>>());

        var request = new SchoolSuggestRequest();

        await _service.Object.SchoolsSuggestAsync(request);

        Assert.NotNull(capturedFields);
        Assert.Equivalent(expected, capturedFields);
    }

    [Fact]
    public async Task SchoolsSuggestAsyncShouldCorrectlyPassSearchText()
    {
        SuggestRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SuggestResponse<SchoolSummary>>>(
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
            .ReturnsAsync(Mock.Of<SuggestResponse<SchoolSummary>>());

        var request = new SchoolSuggestRequest
        {
            SearchText = nameof(SchoolSuggestRequest.SearchText)
        };

        await _service.Object.SchoolsSuggestAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }
}