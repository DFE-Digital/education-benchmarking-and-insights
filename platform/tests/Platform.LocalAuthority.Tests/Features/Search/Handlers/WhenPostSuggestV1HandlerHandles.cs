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

public class WhenPostSuggestV1HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ILocalAuthoritySearchService> _service = new();
    private readonly Mock<IValidator<SuggestRequest>> _validator = new();
    private readonly PostSuggestV1Handler _handler;

    public WhenPostSuggestV1HandlerHandles()
    {
        _handler = new PostSuggestV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var suggestRequest = _fixture.Create<LocalAuthoritySuggestRequest>();
        var suggestResponse = _fixture.Create<SuggestResponse<LocalAuthoritySummaryResponse>>();
        var token = CancellationToken.None;
        var request = MockHttpRequestData.Create(suggestRequest);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<LocalAuthoritySuggestRequest>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.SuggestAsync(It.IsAny<LocalAuthoritySuggestRequest>(), token))
            .ReturnsAsync(suggestResponse);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<SuggestResponse<LocalAuthoritySummaryResponse>>();
        Assert.NotNull(body);
        Assert.Equal(suggestResponse.Results.Count(), body.Results.Count());
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var suggestRequest = _fixture.Create<LocalAuthoritySuggestRequest>();
        var token = CancellationToken.None;
        var request = MockHttpRequestData.Create(suggestRequest);
        var context = new BasicContext(request, token);

        var validationFailures = new[] { new ValidationFailure("SearchText", "Error message") };
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<LocalAuthoritySuggestRequest>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}