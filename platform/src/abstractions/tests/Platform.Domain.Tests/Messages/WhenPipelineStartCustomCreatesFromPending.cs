using Platform.Domain.Messages;
using Xunit;

namespace Platform.Domain.Tests.Messages;

public class WhenPipelineStartCustomCreatesFromPending
{
    [Fact]
    public void ShouldReturnPipelineStartCustomForComparatorSetPayload()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            URN = "urn",
            Payload = new ComparatorSetPipelinePayload
            {
                Set = ["1", "2", "3"]
            }
        };

        var result = PipelineStartCustom.FromPending(pending);

        var expected = new PipelineStartCustom
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            URN = "urn",
            Payload = new ComparatorSetPipelinePayload
            {
                Set = ["1", "2", "3"]
            }
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void ShouldReturnPipelineStartCustomForCustomDataPayload()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            URN = "urn",
            Payload = new CustomDataPipelinePayload
            {
                AdministrativeSuppliesNonEducationalCosts = 1.23m
            }
        };

        var result = PipelineStartCustom.FromPending(pending);

        var expected = new PipelineStartCustom
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = 2020,
            URN = "urn",
            Payload = new CustomDataPipelinePayload
            {
                AdministrativeSuppliesNonEducationalCosts = 1.23m
            }
        };
        Assert.Equivalent(expected, result);
    }

    [Fact]
    public void ShouldThrowExceptionIfYearInvalid()
    {
        var pending = new PipelinePending
        {
            JobId = "jobId",
            RunId = "runId",
            RunType = "runType",
            Type = "type",
            Year = "year",
            URN = "urn",
            Payload = new ComparatorSetPipelinePayload
            {
                Set = ["1", "2", "3"]
            }
        };

        var exception = Assert.Throws<ArgumentException>(() => PipelineStartCustom.FromPending(pending));

        Assert.NotNull(exception);
        Assert.Equal(nameof(PipelinePending.Year), exception.ParamName);
    }
}