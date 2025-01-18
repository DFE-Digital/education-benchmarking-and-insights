using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Search;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class GetSchoolsSuggestFunctionTests : FunctionsTestBase
{
    private readonly GetSchoolsSuggestFunction _function;
    private readonly Mock<ISchoolsService> _service;
    private readonly Mock<IValidator<SuggestRequest>> _validator;

    public GetSchoolsSuggestFunctionTests()
    {
        _service = new Mock<ISchoolsService>();
        _validator = new Mock<IValidator<SuggestRequest>>();
        _function = new GetSchoolsSuggestFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.SuggestAsync(It.IsAny<SchoolSuggestRequest>()))
            .ReturnsAsync(new SuggestResponse<School>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await _function.RunAsync(CreateHttpRequestDataWithBody(new SchoolSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SuggestResponse<School>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn400OnInvalidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SuggestRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([new ValidationFailure(nameof(SuggestRequest.SuggesterName), "This error message")]));

        var result = await _function.RunAsync(CreateHttpRequestDataWithBody(new SchoolSuggestRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(SuggestRequest.SuggesterName));

        _service
            .Verify(d => d.SuggestAsync(It.IsAny<SchoolSuggestRequest>()), Times.Never);
    }
}