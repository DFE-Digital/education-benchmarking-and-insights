using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthority.Features.Accounts.Handlers;
using Platform.Api.LocalAuthority.Features.Accounts.Models;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Handlers;

public class WhenQueryHighNeedsHistoryV1HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IHighNeedsService> _service = new();
    private readonly Mock<IValidator<HighNeedsParameters>> _validator = new();
    private readonly QueryHighNeedsHistoryV1Handler _handler;

    public WhenQueryHighNeedsHistoryV1HandlerHandles()
    {
        _handler = new QueryHighNeedsHistoryV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var history = _fixture.Create<History<HighNeedsYear>>();
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues> { { "code", "LA1" }, { "dimension", "Actuals" } };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator.Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParameters>(), token)).ReturnsAsync(new ValidationResult());
        _service.Setup(s => s.QueryHistoryAsync(It.IsAny<string[]>(), It.IsAny<string>(), token)).ReturnsAsync(history);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<History<HighNeedsYear>>();
        Assert.NotNull(body);
        Assert.Equal(history.StartYear, body.StartYear);
    }

    [Fact]
    public async Task ShouldReturn404WhenYearsNotFound()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues> { { "code", "LA1" } };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator.Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParameters>(), token)).ReturnsAsync(new ValidationResult());
        _service.Setup(s => s.QueryHistoryAsync(It.IsAny<string[]>(), It.IsAny<string>(), token)).ReturnsAsync((History<HighNeedsYear>?)null);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues> { { "code", "" } };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        var validationFailures = new[] { new ValidationFailure("Codes", "Error message") };
        _validator.Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParameters>(), token)).ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}