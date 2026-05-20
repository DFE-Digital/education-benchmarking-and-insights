using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Handlers;

public class WhenQueryMaintainedSchoolWorkforceV1HandlerHandles : HandlerTestBase
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IMaintainedSchoolsService> _service = new();
    private readonly Mock<IValidator<WorkforceSummaryParameters>> _validator = new();
    private readonly QueryMaintainedSchoolWorkforceV1Handler _handler;

    public WhenQueryMaintainedSchoolWorkforceV1HandlerHandles()
    {
        _handler = new QueryMaintainedSchoolWorkforceV1Handler(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var id = "LA1";
        var dtos = _fixture.CreateMany<LocalAuthoritySchoolWorkforceSummaryResponse>().ToList();
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues> { { "limit", "10" } };
        var request = MockHttpRequestData.Create(query, null);
        var context = new IdContext(request, token, id);

        _validator.Setup(v => v.ValidateAsync(It.IsAny<WorkforceSummaryParameters>(), token))
            .ReturnsAsync(new ValidationResult());

        _service.Setup(s => s.GetWorkforceSummaryAsync(id, It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string[]>(), 10, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>(), token))
            .ReturnsAsync(dtos);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<IEnumerable<LocalAuthoritySchoolWorkforceSummaryResponse>>();
        Assert.NotNull(body);
        Assert.Equal(dtos.Count, body.Count());
    }

    [Fact]
    public async Task ShouldReturn404WhenServiceReturnsEmpty()
    {
        var id = "LA1";
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues> { { "dimension", "Actuals" } };
        var request = MockHttpRequestData.Create(query, null);
        var context = new IdContext(request, token, id);

        _validator.Setup(v => v.ValidateAsync(It.IsAny<WorkforceSummaryParameters>(), token))
            .ReturnsAsync(new ValidationResult());

        _service.Setup(s => s.GetWorkforceSummaryAsync(id, It.IsAny<string>(), It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string[]>(), null, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>(), token))
            .ReturnsAsync(Enumerable.Empty<LocalAuthoritySchoolWorkforceSummaryResponse>());

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        var id = "LA1";
        var token = CancellationToken.None;
        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, id);

        var validationFailures = new[] { new ValidationFailure("Limit", "Error message") };
        _validator.Setup(v => v.ValidateAsync(It.IsAny<WorkforceSummaryParameters>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
