using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Trusts;
using Platform.Api.Insight.Features.Trusts.Models;
using Platform.Api.Insight.Features.Trusts.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Trusts;

public class GetTrustsCharacteristicsFunctionTests : FunctionsTestBase
{
    private readonly GetTrustsCharacteristicsFunction _function;
    private readonly Mock<ITrustsService> _service;
    private readonly Fixture _fixture;

    public GetTrustsCharacteristicsFunctionTests()
    {
        _service = new Mock<ITrustsService>();
        _fixture = new Fixture();
        _function = new GetTrustsCharacteristicsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture
            .Build<TrustCharacteristic>()
            .Without(c => c.PhasesCovered)
            .CreateMany(5);

        _service
            .Setup(d => d.QueryAsync(It.IsAny<string[]>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<TrustCharacteristic[]>();
        Assert.NotNull(body);
    }
}