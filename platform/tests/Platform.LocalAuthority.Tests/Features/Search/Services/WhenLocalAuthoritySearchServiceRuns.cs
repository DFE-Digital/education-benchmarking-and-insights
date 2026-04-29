using AutoFixture;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Moq;
using Platform.Api.LocalAuthority.Features.Search.Models;
using Platform.Api.LocalAuthority.Features.Search.Services;
using Platform.Search;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Search.Services;

public class WhenLocalAuthoritySearchServiceRuns
{
    private readonly Mock<IIndexClient> _client = new();
    private readonly LocalAuthoritySearchService _service;
    private readonly Fixture _fixture = new();

    public WhenLocalAuthoritySearchServiceRuns()
    {
        _service = new LocalAuthoritySearchService(_client.Object);
    }

    [Fact]
    public async Task SearchAsyncShouldCallClientWithCorrectOptions()
    {
        var request = _fixture.Create<SearchRequest>();
        var results = _fixture.CreateMany<LocalAuthoritySummaryResponse>(3).ToList();
        var searchResults = SearchModelFactory.SearchResults(
            results.Select(r => SearchModelFactory.SearchResult(r, 1.0, null)).ToList(),
            results.Count,
            null,
            null,
            null);

        SearchOptions? actualOptions = null;
        _client
            .Setup(c => c.SearchAsync<LocalAuthoritySummaryResponse>(request.SearchText, It.IsAny<SearchOptions>(), It.IsAny<CancellationToken>()))
            .Callback<string?, SearchOptions, CancellationToken>((_, options, _) => actualOptions = options)
            .ReturnsAsync(Response.FromValue(searchResults, Mock.Of<Response>()));

        var response = await _service.SearchAsync(request, CancellationToken.None);

        Assert.NotNull(actualOptions);
        Assert.Equal(request.PageSize, actualOptions.Size);
        Assert.Equal((request.Page - 1) * request.PageSize, actualOptions.Skip);
        Assert.Equal(request.FilterExpression(), actualOptions.Filter);
        Assert.Equal(results.Count, response.Results.Count());
        Assert.Equal(results.Count, response.TotalResults);
    }

    [Fact]
    public async Task SuggestAsyncShouldCallClientWithoutFilterWhenExcludeIsEmpty()
    {
        var request = _fixture.Build<LocalAuthoritySuggestRequest>()
            .With(x => x.Exclude, Array.Empty<string>())
            .Create();
        
        var results = _fixture.CreateMany<LocalAuthoritySummaryResponse>(3).ToList();
        var suggestions = results.Select(r => SearchModelFactory.SearchSuggestion(r, r.Name)).ToList();
        var suggestResults = SearchModelFactory.SuggestResults(suggestions, 1.0);

        SuggestOptions? actualOptions = null;
        _client
            .Setup(c => c.SuggestAsync<LocalAuthoritySummaryResponse>(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<SuggestOptions>(), 
                It.IsAny<CancellationToken>()))
            .Callback<string?, string?, SuggestOptions, CancellationToken>((_, _, options, _) => actualOptions = options)
            .ReturnsAsync(Response.FromValue(suggestResults, Mock.Of<Response>()));

        var response = await _service.SuggestAsync(request, CancellationToken.None);

        Assert.NotNull(actualOptions);
        Assert.Null(actualOptions.Filter);
        Assert.Contains(nameof(LocalAuthoritySummaryResponse.Code), actualOptions.Select);
        Assert.Contains(nameof(LocalAuthoritySummaryResponse.Name), actualOptions.Select);
        Assert.Equal(results.Count, response.Results.Count());
    }

    [Fact]
    public async Task SuggestAsyncShouldCallClientWithFilterWhenExcludeIsPopulated()
    {
        var excludeNames = new[] { "LA1", "LA2" };
        var request = _fixture.Build<LocalAuthoritySuggestRequest>()
            .With(x => x.Exclude, excludeNames)
            .Create();
        
        var results = _fixture.CreateMany<LocalAuthoritySummaryResponse>(1).ToList();
        var suggestions = results.Select(r => SearchModelFactory.SearchSuggestion(r, r.Name)).ToList();
        var suggestResults = SearchModelFactory.SuggestResults(suggestions, 1.0);

        SuggestOptions? actualOptions = null;
        _client
            .Setup(c => c.SuggestAsync<LocalAuthoritySummaryResponse>(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<SuggestOptions>(), 
                It.IsAny<CancellationToken>()))
            .Callback<string?, string?, SuggestOptions, CancellationToken>((_, _, options, _) => actualOptions = options)
            .ReturnsAsync(Response.FromValue(suggestResults, Mock.Of<Response>()));

        await _service.SuggestAsync(request, CancellationToken.None);

        Assert.NotNull(actualOptions);
        Assert.Equal("(Name ne 'LA1') and ( Name ne 'LA2')", actualOptions.Filter);
    }
}