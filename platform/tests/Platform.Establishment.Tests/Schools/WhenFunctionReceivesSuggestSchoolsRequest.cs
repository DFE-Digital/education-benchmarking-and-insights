using System.Net;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Functions;
using Platform.Search;
using Platform.Test.Extensions;

using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class WhenFunctionReceivesSuggestSchoolsRequest : SchoolsFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.SuggestAsync(It.IsAny<SchoolSuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<School>());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.SuggestSchoolsAsync(CreateHttpRequestDataWithBody(new SchoolSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<School>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")]));

        var result = await Functions.SuggestSchoolsAsync(CreateHttpRequestDataWithBody(new SchoolSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));
    }
}