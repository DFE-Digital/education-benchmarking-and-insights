using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Requests;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Search;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class PostLocalAuthoritiesSuggestFunctionTests : FunctionsTestBase
{
    private readonly PostLocalAuthoritiesSuggestFunction _function;
    private readonly Mock<ILocalAuthoritiesService> _service;
    private readonly Mock<IValidator<SuggestRequest>> _validator;

    public PostLocalAuthoritiesSuggestFunctionTests()
    {
        _service = new Mock<ILocalAuthoritiesService>();
        _validator = new Mock<IValidator<SuggestRequest>>();
        _function = new PostLocalAuthoritiesSuggestFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.LocalAuthoritiesSuggestAsync(It.IsAny<LocalAuthoritySuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<LocalAuthoritySummary>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(new LocalAuthoritySuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<LocalAuthoritySummary>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")]));

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(new LocalAuthoritySuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(body);
        Assert.Contains(body, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));

        _service
            .Verify(d => d.LocalAuthoritiesSuggestAsync(It.IsAny<LocalAuthoritySuggestRequest>()), Times.Never);
    }
}