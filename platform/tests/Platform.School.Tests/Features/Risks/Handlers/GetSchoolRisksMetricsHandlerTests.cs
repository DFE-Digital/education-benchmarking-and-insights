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

public class WhenGetSchoolRisksMetricsV1HandlerHandles : HandlerTestBase
{
    private readonly Mock<ISchoolRisksService> _service = new();
    private readonly GetSchoolRisksMetricsHandlerV1 _handler;

    public WhenGetSchoolRisksMetricsV1HandlerHandles()
    {
        _handler = new GetSchoolRisksMetricsHandlerV1(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200WhenMetricsExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "123456");

        var metrics = new[]
        {
            new SchoolRisksMetricsResponse
            {
                Urn = "123456",
                RiskGroup = "foo",
                RiskIndicator = "bar",
                RiskIndicatorValue = 1.0m,
                RiskIndicatorFlag = "baz",
                RiskIndicatorContribution = 2.0m,
                RiskIndicatorContributionMax = 3.0m
            },
            new SchoolRisksMetricsResponse
            {
                Urn = "123456",
                RiskGroup = "baz",
                RiskIndicator = "bar",
                RiskIndicatorValue = 4.0m,
                RiskIndicatorFlag = "foo",
                RiskIndicatorContribution = 5.0m,
                RiskIndicatorContributionMax = 6.0m
            }
        };

        _service
            .Setup(s => s.GetMetricsAsync("123456", token))
            .ReturnsAsync(metrics);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<IEnumerable<SchoolRisksMetricsResponse>>();
        Assert.NotNull(body);

        Assert.Equal(metrics, body);
    }

    [Fact]
    public async Task ShouldReturn404WhenNoMetricsExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "999999");

        _service
            .Setup(s => s.GetMetricsAsync("999999", token))
            .ReturnsAsync([]);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
