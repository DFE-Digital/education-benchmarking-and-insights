using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.LocalAuthority.Features.Risks.Handlers;
using Platform.Api.LocalAuthority.Features.Risks.Models;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Api.LocalAuthority.Features.Risks.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Risks.Handlers;

public class WhenQueryEducationHealthCarePlansHistoryV1HandlerHandles : HandlerTestBase
{
    private readonly Mock<ISchoolRisksService> _service = new();
    private readonly Mock<IValidator<PagedSchoolRisksParameters>> _validator = new();
    private readonly QueryPagedSchoolRisksHandlerV1 _handler;

    public WhenQueryEducationHealthCarePlansHistoryV1HandlerHandles()
    {
        _handler = new QueryPagedSchoolRisksHandlerV1(_service.Object, _validator.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var token = CancellationToken.None;
        var query = new Dictionary<string, StringValues>
        {
            { "code", "123" }
        };
        var request = MockHttpRequestData.Create(query, null);
        var context = new BasicContext(request, token);

        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<PagedSchoolRisksParameters>(), token))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(s => s.QueryPagedAsync(
                It.IsAny<string[]>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                token))
            .ReturnsAsync(new PagedResult<SchoolRiskResponse>());

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<PagedResult<SchoolRiskResponse>>();
        Assert.NotNull(body);
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
            .Setup(v => v.ValidateAsync(It.IsAny<PagedSchoolRisksParameters>(), token))
            .ReturnsAsync(new ValidationResult(validationFailures));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
    }
}
