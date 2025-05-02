using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Schools;
using Platform.Api.Insight.Features.Schools.Models;
using Platform.Api.Insight.Features.Schools.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Schools;

public class WhenGetSchoolCharacteristicsFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private readonly GetSchoolCharacteristicsFunction _function;
    private readonly Mock<ISchoolsService> _service = new();

    public WhenGetSchoolCharacteristicsFunctionRuns()
    {
        _function = new GetSchoolCharacteristicsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string urn = nameof(urn);
        var results = Fixture.Create<SchoolCharacteristic>();

        _service
            .Setup(d => d.CharacteristicAsync(urn, It.IsAny<CancellationToken>()))
            .ReturnsAsync(results);

        var result = await _function.RunAsync(CreateHttpRequestData(), urn);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SchoolCharacteristic>();
        Assert.NotNull(body);
        Assert.Equal(results, body);
    }
}