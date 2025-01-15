using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search.Requests;
using Platform.Search.Responses;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class WhenFunctionReceivesSuggestSchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.SuggestAsync(It.IsAny<SuggestRequest>(), It.IsAny<string[]?>()))
            .ReturnsAsync(new SuggestResponse<School>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestSchoolsAsync(CreateHttpRequestDataWithBody(new SuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        result.Headers.TryGetValues("Content-Type", out var header);
        Assert.NotNull(header);
        Assert.Contains(ContentType.ApplicationJson, header);

        var body = await result.ReadAsJsonAsync<SuggestResponse<School>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")]));

        var result = await Functions.SuggestSchoolsAsync(CreateHttpRequestDataWithBody(new SuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        result.Headers.TryGetValues("Content-Type", out var header);
        Assert.NotNull(header);
        Assert.Contains(ContentType.ApplicationJson, header);

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());

        var result = await Functions.SuggestSchoolsAsync(CreateHttpRequestDataWithBody(new SuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}