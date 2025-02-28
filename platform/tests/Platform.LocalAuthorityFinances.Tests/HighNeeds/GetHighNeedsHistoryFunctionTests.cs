using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds;

public class GetHighNeedsHistoryFunctionTests : FunctionsTestBase
{
    private const string Code = nameof(Code);
    private readonly GetHighNeedsHistoryFunction _function;
    private readonly Mock<IHighNeedsHistoryService> _service;
    private readonly Mock<IValidator<HighNeedsHistoryParameters>> _validator;
    private readonly Fixture _fixture;

    public GetHighNeedsHistoryFunctionTests()
    {
        _service = new Mock<IHighNeedsHistoryService>();
        _validator = new Mock<IValidator<HighNeedsHistoryParameters>>();
        _fixture = new Fixture();
        _function = new GetHighNeedsHistoryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<History<LocalAuthorityHighNeedsYear>>().Create();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsHistoryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetHistory(new[] { Code }, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var query = new Dictionary<string, StringValues>
        {
            { "code", Code }
        };

        var result = await _function.RunAsync(CreateHttpRequestData(query), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<History<LocalAuthorityHighNeedsYear>>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsHistoryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetHistory(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((History<LocalAuthorityHighNeedsYear>?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsHistoryParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(HighNeedsHistoryParameters.Codes), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(HighNeedsHistoryParameters.Codes));

        _service
            .Verify(d => d.GetHistory(It.IsAny<string[]>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}