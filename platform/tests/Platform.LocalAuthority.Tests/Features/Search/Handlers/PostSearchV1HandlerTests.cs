using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.LocalAuthority.Features.Search.Handlers;
using Platform.Api.LocalAuthority.Features.Search.Models;
using Platform.Api.LocalAuthority.Features.Search.Services;
using Platform.Functions;
using Platform.Search;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Search.Handlers;

public class PostSearchV1HandlerTests : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ILocalAuthoritySearchService> _service = new();
    private readonly Mock<IValidator<SearchRequest>> _validator = new();
    private readonly PostSearchV1Handler _handler;

    public PostSearchV1HandlerTests()
    {
        _handler = new PostSearchV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var searchRequest = _fixture.Create<SearchRequest>();
        var searchResponse = _fixture.Create<SearchResponse<LocalAuthoritySummaryResponse>>();
        var token = CancellationToken.None;
        var request = MockHttpRequestData.Create(searchRequest);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SearchRequest>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.SearchAsync(It.IsAny<SearchRequest>(), token))
            .ReturnsAsync(searchResponse);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<SearchResponse<LocalAuthoritySummaryResponse>>();
        Assert.NotNull(body);
        Assert.Equal(searchResponse.TotalResults, body.TotalResults);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var searchRequest = _fixture.Create<SearchRequest>();
        var token = CancellationToken.None;
        var request = MockHttpRequestData.Create(searchRequest);
        var context = new BasicContext(request, token);

        var validationFailures = new[] { new ValidationFailure("SearchText", "Error message") };
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SearchRequest>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}