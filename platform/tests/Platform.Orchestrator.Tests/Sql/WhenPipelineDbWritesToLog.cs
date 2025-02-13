using System.Data;
using Moq;
using Platform.Orchestrator.Sql;
using Platform.Sql;
using Xunit;

namespace Platform.Orchestrator.Tests.Sql;

public class WhenPipelineDbWritesToLog
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly PipelineDb _db;
    private readonly Mock<IDbTransaction> _transaction;

    public WhenPipelineDbWritesToLog()
    {
        _transaction = new Mock<IDbTransaction>();

        _connection = new Mock<IDatabaseConnection>();
        _connection
            .Setup(c => c.BeginTransaction())
            .Returns(_transaction.Object);

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory
            .Setup(d => d.GetConnection())
            .ReturnsAsync(_connection.Object);

        _db = new PipelineDb(dbFactory.Object);
    }

    [Theory]
    [InlineData("12345", "message")]
    public async Task ShouldInsertCompletedPipelineRunIntoDatabase(string id, string message)
    {
        const int identity = 1;
        _connection
            .Setup(c => c.InsertAsync(
                It.Is<CompletedPipelineRun>(p => p.OrchestrationId == id && p.Message == message && p.CompletedAt > DateTimeOffset.MinValue),
                _transaction.Object))
            .ReturnsAsync(identity);

        var result = await _db.WriteToLog(id, message);
        Assert.Equal(identity, result);
    }
}