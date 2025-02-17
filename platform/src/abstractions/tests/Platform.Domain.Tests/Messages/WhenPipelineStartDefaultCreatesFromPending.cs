using Newtonsoft.Json.Linq;
using Platform.Domain.Messages;
using Xunit;

namespace Platform.Domain.Tests.Messages;

public class WhenPipelineStartDefaultCreatesFromPending
{
    [Fact]
    public void ShouldReturnPipelineStartDefault()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = 2020,
            RunType = "runType",
            Type = "type",
            Year = JObject.FromObject(new PipelineMessageYears
            {
                Aar = 2021,
                Bfr = 2022,
                Cfr = 2023
            })
        };

        var result = PipelineStartDefault.FromPending(pending);

        var expected = new PipelineStartDefault
        {
            JobId = "jobId",
            RunId = 2020,
            RunType = "runType",
            Type = "type",
            Year = new PipelineMessageYears
            {
                Aar = 2021,
                Bfr = 2022,
                Cfr = 2023
            }
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void ShouldThrowExceptionIfRunIdInvalid()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = "year"
        };

        var exception = Assert.Throws<ArgumentException>(() => PipelineStartDefault.FromPending(pending));

        Assert.NotNull(exception);
        Assert.Equal(nameof(PipelinePending.RunId), exception.ParamName);
    }

    [Fact]
    public void ShouldThrowExceptionIfYearInvalid()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = "2020",
            RunType = "runType",
            Type = "type",
            Year = "year"
        };

        var exception = Assert.Throws<ArgumentException>(() => PipelineStartDefault.FromPending(pending));

        Assert.NotNull(exception);
        Assert.Equal(nameof(PipelinePending.Year), exception.ParamName);
    }
}