using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Search;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class PostSchoolsSearchFunctionTests : FunctionsTestBase
{
    private readonly PostSchoolsSearchFunction _function;
    private readonly Mock<ISchoolsService> _service;
    private readonly Mock<IValidator<SearchRequest>> _validator;

    public PostSchoolsSearchFunctionTests()
    {
        _service = new Mock<ISchoolsService>();
        _validator = new Mock<IValidator<SearchRequest>>();
        _function = new PostSchoolsSearchFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.SchoolsSearchAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SearchResponse<SchoolSummary>());

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await _function.RunAsync(CreateHttpRequestDataWithBody(new SearchRequest()));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SearchResponse<SchoolSummary>>();
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
            .Verify(d => d.SchoolsSearchAsync(It.IsAny<SearchRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}