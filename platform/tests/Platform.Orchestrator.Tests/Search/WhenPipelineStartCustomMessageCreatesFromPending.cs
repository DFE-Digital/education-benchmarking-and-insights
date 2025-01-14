using AutoFixture;
using Platform.Api.Benchmark.CustomData;
using Platform.Domain.Messages;
using Xunit;

namespace Platform.Orchestrator.Tests.Search;

public class WhenPipelineStartCustomMessageCreatesFromPending
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldCreatePipelineStartCustomMessage()
    {
        var runId = Guid.NewGuid().ToString();
        const string runType = PipelineRunType.Custom;
        const string type = PipelineJobType.CustomData;
        const string urn = nameof(urn);
        const int year = 2024;
        var payload = _fixture.Create<CustomDataRequest>().CreatePayload();

        var input = new PipelinePending
        {
            RunId = runId,
            RunType = runType,
            Type = type,
            URN = urn,
            Year = year,
            Payload = payload
        };

        var result = PipelineStartCustom.FromPending(input);
        Assert.Equal(runId, result.RunId);
        Assert.Equal(runType, result.RunType);
        Assert.Equal(type, result.Type);
        Assert.Equal(urn, result.URN);
        Assert.Equal(year, result.Year);
        Assert.Equal(payload, result.Payload);
    }

    [Theory]
    [InlineData("year", "Unable to parse `year` as `int` (Parameter 'Year')")]
    public void ShouldNotCreatePipelineStartCustomMessageIfYearInWrongFormat(object? year, string expectedMessage)
    {
        var runId = Guid.NewGuid().ToString();
        const string runType = PipelineRunType.Custom;
        const string type = PipelineJobType.CustomData;
        const string urn = nameof(urn);
        var payload = _fixture.Create<CustomDataRequest>().CreatePayload();

        var input = new PipelinePending
        {
            RunId = runId,
            RunType = runType,
            Type = type,
            URN = urn,
            Year = year,
            Payload = payload
        };

        var exception = Assert.Throws<ArgumentException>(() => PipelineStartCustom.FromPending(input));
        Assert.Equal(expectedMessage, exception.Message);
    }
}