using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Functions;
using Platform.Infrastructure.Search;
using Xunit;

namespace Platform.Tests.Establishment;

public class WhenFunctionReceivesSuggestLocalAuthoritiesRequest : LocalAuthoritiesFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.SuggestAsync(It.IsAny<PostSuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<LocalAuthority>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new PostSuggestRequest())) as
                JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
                { new ValidationFailure(nameof(PostSuggestRequest.SuggesterName), "This error message") }));

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new PostSuggestRequest())) as
                ValidationErrorsResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);

        var values = result.Value as IEnumerable<ValidationError>;
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(PostSuggestRequest.SuggesterName));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<PostSuggestRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new PostSuggestRequest())) as
                StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}