using AutoFixture;
using Newtonsoft.Json.Linq;
using Platform.Domain;
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
        const string type = Pipeline.JobType.Default;
        const string runUntil = "pre-processing";
        const bool generateTransparencyFilesAndPrecursorFiles = true;
        var year = _fixture.Create<PipelineMessageYears>();

        var input = new PipelinePending
        {
            RunId = runId,
            Type = type,
            RunUntil = runUntil,
            GenerateTransparencyFilesAndPrecursorFiles = generateTransparencyFilesAndPrecursorFiles,
            Year = JObject.FromObject(year)
        };

        var result = PipelineStartDefault.FromPending(input);
        Assert.Equal(runId, result.RunId);
        Assert.Equal(type, result.Type);
        Assert.Equal(year, result.Year);
        Assert.Equal(runUntil, result.RunUntil);
        Assert.Equal(generateTransparencyFilesAndPrecursorFiles, result.GenerateTransparencyFilesAndPrecursorFiles);
    }

    [Theory]
    [InlineData(2024, 2024, "Unable to parse `2024` as `PipelineMessageYears` (Parameter 'Year')")]
    [InlineData(2024, null, "Unable to parse `` as `PipelineMessageYears` (Parameter 'Year')")]
    [InlineData("runId", null, "Unable to parse `runId` as `int` (Parameter 'RunId')")]
    [InlineData(null, null, "Unable to parse `` as `int` (Parameter 'RunId')")]
    public void ShouldNotCreatePipelineStartDefaultMessageIfYearInWrongFormat(object? runId, object? year, string expectedMessage)
    {
        const string type = Pipeline.JobType.Default;
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
