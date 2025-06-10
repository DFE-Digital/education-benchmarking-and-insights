using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Years;
using Platform.Api.Content.Features.Years.Models;
using Platform.Api.Content.Features.Years.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.Years;

public class GetCurrentReturnYearsFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetCurrentReturnYearsFunction _function;
    private readonly Mock<IYearsService> _service;

    public GetCurrentReturnYearsFunctionTests()
    {
        _service = new Mock<IYearsService>();
        _function = new GetCurrentReturnYearsFunction(_service.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var years = _fixture.Create<FinanceYears>();

        _service
            .Setup(x => x.GetCurrentReturnYears(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(years);

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<FinanceYears>();
        Assert.NotNull(body);
        Assert.Equivalent(years, body);
    }
}