using Moq;
using Moq.Protected;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Search;
using Platform.Sql;
using Xunit;

namespace Platform.Establishment.Tests.Schools.Services;

public class WhenSchoolsServiceIsCalled
{
    private readonly Mock<IIndexClient> _client = new();
    private readonly Mock<IDatabaseFactory> _db = new();
    private readonly Mock<SchoolsService> _service;

    public WhenSchoolsServiceIsCalled()
    {
        _service = new Mock<SchoolsService>(_client.Object, _db.Object) { CallBase = true };
    }

    [Fact]
    public async Task SchoolSearchesAsyncShouldCorrectlyPassFacets()
    {
        string[]? capturedFacets = null;

        _service.Protected()
            .Setup<Task<SearchResponse<School>>>(
                "SearchAsync",
                ItExpr.IsAny<SearchRequest>(),
                ItExpr.IsAny<Func<FilterCriteria[], string?>>(),
                ItExpr.IsAny<string[]?>()
            )
            .Callback((SearchRequest _, Func<FilterCriteria[], string?> _, string[] facets) =>
            {
                capturedFacets = facets;
            })
            .ReturnsAsync(Mock.Of<SearchResponse<School>>());

        var request = new SearchRequest();

        await _service.Object.SchoolsSearchAsync(request);

        Assert.NotNull(capturedFacets);
        Assert.Contains(nameof(School.OverallPhase), capturedFacets);
    }

    [Fact]
    public async Task SchoolSearchesAsyncShouldCorrectlyPassSearchText()
    {
        SearchRequest? capturedRequest = null;

        _service.Protected()
            .Setup<Task<SearchResponse<School>>>(
                "SearchAsync",
                ItExpr.IsAny<SearchRequest>(),
                ItExpr.IsAny<Func<FilterCriteria[], string?>>(),
                ItExpr.IsAny<string[]?>()
            )
            .Callback((SearchRequest request, Func<FilterCriteria[], string?> _, string[] _) =>
            {
                capturedRequest = request;
            })
            .ReturnsAsync(Mock.Of<SearchResponse<School>>());

        var request = new SearchRequest
        {
            SearchText = nameof(SearchRequest.SearchText)
        };

        await _service.Object.SchoolsSearchAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData($"({nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}')", 1)]
    [InlineData($"({nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}' or {nameof(FilterCriteria.Field)} eq '{nameof(FilterCriteria.Value)}')", 2)]
    public async Task SchoolSearchesAsyncShouldCorrectlyPassFilterExpBuilderThatCreatesCorrectString(string? expected, int filterCount)
    {
        Func<FilterCriteria[], string?>? capturedFilterExpBuilder = null;

        _service.Protected()
            .Setup<Task<SearchResponse<School>>>(
                "SearchAsync",
                ItExpr.IsAny<SearchRequest>(),
                ItExpr.IsAny<Func<FilterCriteria[], string?>>(),
                ItExpr.IsAny<string[]?>()
            )
            .Callback((SearchRequest _, Func<FilterCriteria[], string?> filterExpBuilder, string[] _) =>
            {
                capturedFilterExpBuilder = filterExpBuilder;
            })
            .ReturnsAsync(Mock.Of<SearchResponse<School>>());

        var request = new SearchRequest
        {
            SearchText = nameof(SearchRequest.SearchText)
        };

        await _service.Object.SchoolsSearchAsync(request);

        Assert.NotNull(capturedFilterExpBuilder);
        SearchFilterExpBuilderShouldBuildCorrectStringWithFilters(capturedFilterExpBuilder, expected, filterCount);
    }

    [Fact]
    public async Task SchoolsSuggestAsyncShouldCorrectlyPassFields()
    {
        string[]? capturedFields = null;

        var expected = new[]
        {
            nameof(School.SchoolName),
            nameof(School.URN),
            nameof(School.AddressStreet),
            nameof(School.AddressLocality),
            nameof(School.AddressLine3),
            nameof(School.AddressTown),
            nameof(School.AddressCounty),
            nameof(School.AddressPostcode)
        };

        _service.Protected()
            .Setup<Task<SuggestResponse<School>>>(
                "SuggestAsync",
                ItExpr.IsAny<SuggestRequest>(),
                ItExpr.IsAny<Func<string?>?>(),
                ItExpr.IsAny<string[]?>()
            )
            .Callback((SuggestRequest _, Func<string?>? _, string[] fields) =>
            {
                capturedFields = fields;
            })
            .ReturnsAsync(Mock.Of<SuggestResponse<School>>());

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
            .Setup<Task<SuggestResponse<School>>>(
                "SuggestAsync",
                ItExpr.IsAny<SuggestRequest>(),
                ItExpr.IsAny<Func<string?>?>(),
                ItExpr.IsAny<string[]?>()
            )
            .Callback((SuggestRequest request, Func<string?>? _, string[] _) =>
            {
                capturedRequest = request;
            })
            .ReturnsAsync(Mock.Of<SuggestResponse<School>>());

        var request = new SchoolSuggestRequest
        {
            SearchText = nameof(SchoolSuggestRequest.SearchText)
        };

        await _service.Object.SchoolsSuggestAsync(request);

        Assert.NotNull(capturedRequest);
        Assert.Contains(request.SearchText, capturedRequest.SearchText);
    }

    private static void SearchFilterExpBuilderShouldBuildCorrectStringWithFilters(Func<FilterCriteria[], string?> filterExpBuilder, string? expected, int filterCount)
    {
        var filterCriteria = Enumerable.Range(0, filterCount)
            .Select(_ => new FilterCriteria
            {
                Field = nameof(FilterCriteria.Field),
                Value = nameof(FilterCriteria.Value)
            })
            .ToArray();

        var result = filterExpBuilder.Invoke(filterCriteria);

        Assert.Equal(expected, result);
    }
}