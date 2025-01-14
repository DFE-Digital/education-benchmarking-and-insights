using AutoFixture;
using Newtonsoft.Json.Linq;
using Platform.Domain.Messages;
using Xunit;

namespace Platform.Orchestrator.Tests.Search;

public class WhenPipelineStartDefaultMessageCreatesFromPending
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldCreatePipelineStartDefaultMessage()
    {
        const int runId = 2024;
        const string type = PipelineJobType.Default;
        var year = _fixture.Create<PipelineMessageYears>();

        var input = new PipelinePending
        {
            RunId = runId,
            Type = type,
            Year = JObject.FromObject(year)
        };

        var result = PipelineStartDefault.FromPending(input);
        Assert.Equal(runId, result.RunId);
        Assert.Equal(type, result.Type);
        Assert.Equal(year, result.Year);
    }

    [Theory]
    [InlineData(2024, 2024, "Unable to parse `2024` as `PipelineMessageYears` (Parameter 'Year')")]
    [InlineData(2024, null, "Unable to parse `` as `PipelineMessageYears` (Parameter 'Year')")]
    [InlineData("runId", null, "Unable to parse `runId` as `int` (Parameter 'RunId')")]
    [InlineData(null, null, "Unable to parse `` as `int` (Parameter 'RunId')")]
    public void ShouldNotCreatePipelineStartDefaultMessageIfYearInWrongFormat(object? runId, object? year, string expectedMessage)
    {
        const string type = PipelineJobType.Default;
        var input = new PipelinePending
        {
            RunId = runId,
            Type = type,
            Year = year
        };

        var exception = Assert.Throws<ArgumentException>(() => PipelineStartDefault.FromPending(input));
        Assert.Equal(expectedMessage, exception.Message);
    }
}