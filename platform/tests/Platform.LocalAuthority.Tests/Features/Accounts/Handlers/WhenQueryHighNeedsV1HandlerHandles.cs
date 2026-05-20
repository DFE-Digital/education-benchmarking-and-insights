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

public class WhenQueryHighNeedsV1HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IHighNeedsService> _service = new();
    private readonly Mock<IValidator<HighNeedsParametersV1>> _validator = new();
    private readonly QueryHighNeedsV1Handler _handler;

    public WhenQueryHighNeedsV1HandlerHandles()
    {
        _handler = new QueryHighNeedsV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var dtos = _fixture.CreateMany<LocalAuthority<HighNeeds>>().ToArray();
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "LA1" },
            { "dimension", "Actuals" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParametersV1>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QueryAsync(It.IsAny<string[]>(), It.IsAny<string>(), token))
            .ReturnsAsync(dtos);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<LocalAuthority<HighNeeds>[]>();
        Assert.NotNull(body);
        Assert.Equal(dtos.Length, body.Length);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        var validationFailures = new[] { new ValidationFailure("Codes", "Error message") };
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParametersV1>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
