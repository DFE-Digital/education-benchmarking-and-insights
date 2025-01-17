using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Functions;
using Platform.Test.Extensions;
using Platform.Search;

using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class WhenFunctionReceivesSuggestLocalAuthoritiesRequest : LocalAuthoritiesFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.SuggestAsync(It.IsAny<LocalAuthoritySuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<LocalAuthority>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result = await Functions.SuggestLocalAuthoritiesAsync(CreateHttpRequestDataWithBody(new LocalAuthoritySuggestRequest()));
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<LocalAuthority>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")]));

        var result = await Functions.SuggestLocalAuthoritiesAsync(CreateHttpRequestDataWithBody(new LocalAuthoritySuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(body);
        Assert.Contains(body, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));

        Service
            .Verify(d => d.SuggestAsync(It.IsAny<LocalAuthoritySuggestRequest>()), Times.Never);
    }
}