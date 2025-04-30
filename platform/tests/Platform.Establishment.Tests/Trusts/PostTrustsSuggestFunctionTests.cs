using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Test.Extensions;
using Platform.Search;
using Platform.Test;
using Xunit;

namespace Platform.Establishment.Tests.Trusts;

public class PostTrustsSuggestFunctionTests : FunctionsTestBase
{
    private readonly PostTrustsSuggestFunction _function;
    private readonly Mock<ITrustsService> _service;
    private readonly Mock<IValidator<SuggestRequest>> _validator;

    public PostTrustsSuggestFunctionTests()
    {
        _service = new Mock<ITrustsService>();
        _validator = new Mock<IValidator<SuggestRequest>>();
        _function = new PostTrustsSuggestFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.TrustsSuggestAsync(It.IsAny<TrustSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SuggestResponse<TrustSummary>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await _function.RunAsync(CreateHttpRequestDataWithBody(new TrustSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<TrustSummary>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(new TrustSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));

        _service
            .Verify(d => d.TrustsSuggestAsync(It.IsAny<TrustSuggestRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}