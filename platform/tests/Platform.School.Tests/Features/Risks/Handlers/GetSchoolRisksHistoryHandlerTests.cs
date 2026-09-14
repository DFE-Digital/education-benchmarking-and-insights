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

public class WhenGetSchoolRisksHistoryV1HandlerHandles : HandlerTestBase
{
    private readonly Mock<ISchoolRisksService> _service = new();
    private readonly GetSchoolRisksHistoryHandlerV1 _handler;

    public WhenGetSchoolRisksHistoryV1HandlerHandles()
    {
        _handler = new GetSchoolRisksHistoryHandlerV1(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200WhenHistoryExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "123456");

        var years = new YearsModelDto
        {
            StartYear = 2020,
            EndYear = 2021
        };

        var rows = new[]
        {
            new RisksHistoryModelDto
            {
                Urn = "123456",
                SchoolName = "Test School",
                OverallGrade = "A",
                Overall = 1.0m,
                Financial = 2.0m,
                SchoolAndPupil = 3.0m,
                EducationalPerformance = 4.0m,
                RunId = 2020
            },
            new RisksHistoryModelDto
            {
                Urn = "123456",
                SchoolName = "Test School",
                OverallGrade = "A",
                Overall = 1.1m,
                Financial = 2.1m,
                SchoolAndPupil = 3.1m,
                EducationalPerformance = 4.1m,
                RunId = 2021
            }
        };

        _service
            .Setup(s => s.GetHistoryAsync("123456", token))
            .ReturnsAsync((years, rows));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<SchoolRisksHistoryResponse>();
        Assert.NotNull(body);

        Assert.Equal(2020, body.StartYear);
        Assert.Equal(2021, body.EndYear);
        Assert.Equal(2, body.Rows.Count());
    }

    [Fact]
    public async Task ShouldReturn404WhenNoHistoryExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "999999");

        _service
            .Setup(s => s.GetHistoryAsync("999999", token))
            .ReturnsAsync((null, []));

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
