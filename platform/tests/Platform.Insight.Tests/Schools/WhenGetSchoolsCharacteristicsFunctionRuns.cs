using System.Net;
using AutoFixture;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Features.Schools;
using Platform.Api.Insight.Features.Schools.Models;
using Platform.Api.Insight.Features.Schools.Parameters;
using Platform.Api.Insight.Features.Schools.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Schools;

public class WhenGetSchoolsCharacteristicsFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private static readonly SchoolsParameters QueryParams = Fixture.Create<SchoolsParameters>();
    private readonly GetSchoolsCharacteristicsFunction _function;
    private readonly Dictionary<string, StringValues> _query = new()
    {
        { nameof(QueryParams.Schools), QueryParams.Schools }
    };
    private readonly Mock<ISchoolsService> _service = new();

    public WhenGetSchoolsCharacteristicsFunctionRuns()
    {
        _function = new GetSchoolsCharacteristicsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var results = Fixture.Build<SchoolCharacteristic>().CreateMany().ToArray();

        _service
            .Setup(d => d.QueryCharacteristicAsync(QueryParams.Schools, It.IsAny<CancellationToken>()))
            .ReturnsAsync(results);

        var result = await _function.RunAsync(CreateHttpRequestData(_query));

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SchoolCharacteristic[]>();
        Assert.NotNull(body);
        Assert.Equal(results, body);
    }
}