using System.Data;
using Moq;
using Platform.Orchestrator.Functions;
using Platform.Orchestrator.Sql;
using Platform.Sql;
using Xunit;

namespace Platform.Orchestrator.Tests.Sql;

public class WhenPipelineDbUpdatesStatus
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly PipelineDb _db;
    private readonly Mock<IDbTransaction> _transaction;

    public WhenPipelineDbUpdatesStatus()
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
    [InlineData("12345", true, "complete")]
    [InlineData("67890", false, "failed")]
    public async Task ShouldUpdatePipelineStatusInDatabase(string runId, bool success, string expectedStatus)
    {
        var status = new PipelineStatus
        {
            RunId = runId,
            Success = success
        };
        const int rowsAffected = 1;

        string? actualSql = null;
        string? actualRunId = null;
        string? actualStatus = null;
        _connection
            .Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object>(), _transaction.Object, It.IsAny<CancellationToken>()))
            .Callback<string, object?, IDbTransaction?, CancellationToken>((sql, param, _, _) =>
            {
                actualSql = sql;
                actualRunId = param?.GetType().GetProperty("RunId")?.GetValue(param) as string;
                actualStatus = param?.GetType().GetProperty("status")?.GetValue(param) as string;
            })
            .ReturnsAsync(rowsAffected);

        var result = await _db.UpdateStatus(status);
        Assert.Equal("UPDATE UserData SET Status = @status where Id = @RunId", actualSql);
        Assert.Equal(rowsAffected, result);
        Assert.Equal(runId, actualRunId);
        Assert.Equal(expectedStatus, actualStatus);
    }
}