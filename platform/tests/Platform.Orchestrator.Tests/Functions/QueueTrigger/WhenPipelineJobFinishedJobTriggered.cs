using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Orchestrator.Configuration;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Sql;
using Platform.Orchestrator.Storage;
using Platform.Orchestrator.Telemetry;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Orchestrator.Tests.Functions.QueueTrigger;

public class WhenPipelineJobFinishedJobTriggered(ITestOutputHelper testOutputHelper) : QueueTriggerFunctionTest(testOutputHelper)
{
    private readonly Mock<ILogger<PipelineQueueTriggerFunctions>> _loggerMock = new();
    private readonly Mock<IPipelineDb> _dbMock = new();
    private readonly Mock<ITelemetryService> _telemetryServiceMock = new();
    private readonly Mock<IBlobStorageService> _blobStorageServiceMock = new();
    private readonly Mock<IOptions<StorageOptions>> _storageOptionsMock = new();

    private PipelineQueueTriggerFunctions CreateSut()
    {
        return new PipelineQueueTriggerFunctions(
            _loggerMock.Object,
            _dbMock.Object,
            _telemetryServiceMock.Object,
            _blobStorageServiceMock.Object,
            _storageOptionsMock.Object
        );
    }

    [Fact]
    public async Task ShouldWriteToLog()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            Success = true
        };

        var json = message.ToJson();

        _dbMock
            .Setup(d => d.WriteToLog(message.JobId, json))
            .ReturnsAsync(1)
            .Verifiable();

        var client = new Mock<DurableTaskClient>("name");
        var sut = CreateSut();

        await sut.PipelineJobFinished(json, client.Object);

        _dbMock.Verify();
    }

    [Fact]
    public async Task ShouldRaiseEvent()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            Success = true
        };

        var client = new Mock<DurableTaskClient>("name");

        client
            .Setup(c => c.RaiseEventAsync(message.JobId, nameof(PipelineQueueTriggerFunctions.PipelineJobFinished), message.Success, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var sut = CreateSut();

        await sut.PipelineJobFinished(message.ToJson(), client.Object);

        client.Verify();
    }

    [Fact]
    public async Task ShouldMoveLaaBlob()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            RunId = "2024",
            Success = true,
            DeriveLaaRiskScores = true
        };

        var options = new StorageOptions
        {
            LaaRiskFiles = new BlobMoveTaskOptions
            {
                SourceEndpoint = "src",
                SourceContainer = "src-container",
                DestEndpoint = "dest",
                DestContainer = "dest-container"
            }
        };

        _storageOptionsMock.Setup(s => s.Value).Returns(options);

        _blobStorageServiceMock
            .Setup(b => b.MoveBlobAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(new BlobMoveResult
            {
                Success = true,
                FileName = "laa_risk_scores_download_2023_2024.csv",
                FilesizeMb = 1.5m,
                SourceContainer = "src-container",
                DestContainer = "dest-container"
            })
            .Verifiable();

        var client = new Mock<DurableTaskClient>("name");
        var sut = CreateSut();

        await sut.PipelineJobFinished(message.ToJson(), client.Object);

        _blobStorageServiceMock.Verify();
    }

    [Fact]
    public async Task ShouldWriteLaaFileRecord()
    {
        var message = new PipelineFinish
        {
            JobId = "jobId",
            RunId = "2024",
            Success = true,
            DeriveLaaRiskScores = true
        };

        var options = new StorageOptions
        {
            LaaRiskFiles = new BlobMoveTaskOptions
            {
                SourceEndpoint = "https://src.blob.core.windows.net",
                SourceContainer = "src-container",
                DestEndpoint = "https://dest.blob.core.windows.net",
                DestContainer = "dest-container"
            }
        };

        _storageOptionsMock.Setup(s => s.Value).Returns(options);

        _blobStorageServiceMock
            .Setup(b => b.MoveBlobAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new BlobMoveResult
            {
                Success = true,
                FileName = "laa_risk_scores_download_2023_2024.csv",
                FilesizeMb = 2.4m
            });

        _dbMock
            .Setup(d => d.WriteLocalAuthorityRisksFileDetails(It.Is<FileRecord>(f =>
                f.Type == "laa-risk-scores" &&
                f.Label == "Download detailed risk data for all schools" &&
                f.FileName == "laa_risk_scores_download_2023_2024.csv" &&
                f.RunId == "2024" &&
                f.Filesize == 2.4m)))
            .ReturnsAsync(1)
            .Verifiable();

        var client = new Mock<DurableTaskClient>("name");
        var sut = CreateSut();

        await sut.PipelineJobFinished(message.ToJson(), client.Object);

        _dbMock.Verify();
    }

    [Fact]
    public void ShouldReceiveMessagesFromFinishedQueueFromConfiguration()
    {
        var method = typeof(PipelineQueueTriggerFunctions).GetMethod(nameof(PipelineQueueTriggerFunctions.PipelineJobFinished));
        var parameters = method?.GetParameters();
        var messageParam = parameters?.SingleOrDefault(p => p.Name == "message");
        var attribute = messageParam?.GetCustomAttributes(typeof(QueueTriggerAttribute), false).FirstOrDefault() as QueueTriggerAttribute;

        Assert.NotNull(attribute);
        Assert.Equal("PipelineMessageHub:ConnectionString", attribute.Connection);
        Assert.Equal("%PipelineMessageHub:JobFinishedQueue%", attribute.QueueName);
    }
}
