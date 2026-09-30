using System.Net;
using Moq;
using Platform.Api.School.Features.Risks.Handlers;
using Platform.Api.School.Features.Risks.Models;
using Platform.Api.School.Features.Risks.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.School.Tests.Features.Risks.Handlers;

public class WhenGetSchoolRisksV1HandlerHandles : HandlerTestBase
{
    private readonly Mock<ISchoolRisksService> _service = new();
    private readonly GetSchoolRisksHandlerV1 _handler;

    public WhenGetSchoolRisksV1HandlerHandles()
    {
        _handler = new GetSchoolRisksHandlerV1(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200WhenExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "123456");

        var risks = new SchoolRisksResponse
        {
            Urn = "123456",
            SchoolName = "Test School",
            OverallGrade = "A",
            Overall = 1.0m,
            OverallMax = 2.0m,
            Financial = 2.0m,
            FinancialMax = 3.0m,
            SchoolAndPupil = 3.0m,
            SchoolAndPupilMax = 4.0m,
            EducationalPerformance = 4.0m,
            EducationalPerformanceMax = 5.0m
        };

        _service
            .Setup(s => s.GetAsync("123456", token))
        .ReturnsAsync(risks);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<SchoolRisksResponse>();
        Assert.NotNull(body);

        Assert.Equal(risks, body);
    }

    [Fact]
    public async Task ShouldReturn404WhenNoRisksExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "999999");

        SchoolRisksResponse? nullResponse = null;
        _service
            .Setup(s => s.GetAsync("999999", token))
            .ReturnsAsync(nullResponse);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
