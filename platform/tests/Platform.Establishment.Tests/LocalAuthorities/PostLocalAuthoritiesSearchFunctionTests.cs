using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Search;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class PostLocalAuthoritiesSearchFunctionTests : FunctionsTestBase
{
    private readonly PostLocalAuthoritiesSearchFunction _function;
    private readonly Mock<ILocalAuthoritiesService> _service;
    private readonly Mock<IValidator<SearchRequest>> _validator;

    public PostLocalAuthoritiesSearchFunctionTests()
    {
        _service = new Mock<ILocalAuthoritiesService>();
        _validator = new Mock<IValidator<SearchRequest>>();
        _function = new PostLocalAuthoritiesSearchFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.LocalAuthoritiesSearchAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SearchResponse<LocalAuthoritySummary>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await _function.RunAsync(CreateHttpRequestDataWithBody(new SearchRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SearchResponse<LocalAuthoritySummary>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(SearchRequest.SearchText), "error")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(new SearchRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SearchRequest.SearchText));

        _service
            .Verify(d => d.LocalAuthoritiesSearchAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}