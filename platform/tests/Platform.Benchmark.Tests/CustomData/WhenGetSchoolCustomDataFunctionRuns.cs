using AutoFixture;
using Moq;
using Platform.Api.Benchmark.Features.CustomData;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Benchmark.Tests.CustomData;

public class WhenGetSchoolCustomDataFunctionRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly GetSchoolCustomDataFunction _function;
    private readonly Mock<ICustomDataService> _service = new();

    public WhenGetSchoolCustomDataFunctionRuns()
    {
        _function = new GetSchoolCustomDataFunction(_service.Object);
    }

    [Fact]
    public async Task CustomShouldBeOkOnValidRequest()
    {
        const string urn = nameof(urn);
        const string identifier = nameof(identifier);
        var result = _fixture.Create<CustomDataSchool>();

        _service
            .Setup(d => d.CustomDataSchoolAsync(urn, identifier))
            .ReturnsAsync(result);

        var response =
            await _function.RunAsync(CreateHttpRequestData(), urn, identifier);

        var actual = await response.ReadAsJsonAsync<CustomDataSchool>();
        Assert.NotNull(actual);
        Assert.Equivalent(result, actual);
    }
}