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
            .Setup(d => d.SuggestAsync(It.IsAny<SuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<LocalAuthority>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new SuggestRequest())) as
                JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[]
                { new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message") }));

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new SuggestRequest())) as
                ValidationErrorsResult;

        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);

        var values = result.Value as IEnumerable<ValidationError>;
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());

        var result =
            await Functions.SuggestLocalAuthoritiesAsync(CreateRequestWithBody(new SuggestRequest())) as
                StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result.StatusCode);
    }
}