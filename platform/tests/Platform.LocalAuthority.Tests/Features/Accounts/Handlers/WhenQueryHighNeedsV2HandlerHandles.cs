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

public class WhenQueryHighNeedsV2HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IHighNeedsService> _service = new();
    private readonly Mock<IValidator<HighNeedsParametersV2>> _validator = new();
    private readonly QueryHighNeedsV2Handler _handler;

    public WhenQueryHighNeedsV2HandlerHandles()
    {
        _handler = new QueryHighNeedsV2Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("2.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var dtos = _fixture.Create<HighNeedsResponse[]>();
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "LA1" },
            { "dimension", "PerPupil" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParametersV2>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QueryByTransactionTypeAsync(It.IsAny<string[]>(), It.IsAny<string>(), It.IsAny<string>(), token))
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
            .Setup(v => v.ValidateAsync(It.IsAny<HighNeedsParametersV2>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
