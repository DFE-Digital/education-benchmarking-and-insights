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

public class GetTrustsSuggestFunctionTests : FunctionsTestBase
{
    private readonly GetTrustsSuggestFunction _functions;
    private readonly Mock<ITrustsService> _service;
    private readonly Mock<IValidator<SuggestRequest>> _validator;

    public GetTrustsSuggestFunctionTests()
    {
        _service = new Mock<ITrustsService>();
        _validator = new Mock<IValidator<SuggestRequest>>();
        _functions = new GetTrustsSuggestFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.SuggestAsync(It.IsAny<TrustSuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<Trust>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await _functions.RunAsync(CreateHttpRequestDataWithBody(new TrustSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<Trust>>();
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

        var result = await _functions.RunAsync(CreateHttpRequestDataWithBody(new TrustSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));

        _service
            .Verify(d => d.SuggestAsync(It.IsAny<TrustSuggestRequest>()), Times.Never);
    }
}