using System.Data;
using Moq;
using Platform.Orchestrator.Sql;
using Platform.Sql;
using Xunit;

namespace Platform.Orchestrator.Tests.Sql;

public class WhenPipelineDbDeactivatesUserData
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly PipelineDb _db;
    private readonly Mock<IDbTransaction> _transaction;

    public WhenPipelineDbDeactivatesUserData()
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

    [Fact]
    public async Task ShouldDeactivateUserDataInDatabase()
    {
        const int rowsAffected = 1;

        string? actualSql = null;
        _connection
            .Setup(c => c.ExecuteAsync(It.IsAny<string>(), It.IsAny<object?>(), _transaction.Object, It.IsAny<CancellationToken>()))
            .Callback<string, object?, IDbTransaction?, CancellationToken>((sql, _, _, _) =>
            {
                actualSql = sql;
            })
            .ReturnsAsync(rowsAffected);

        var result = await _db.DeactivateUserData();
        Assert.Equal("UPDATE UserData SET Active = 0", actualSql);
        Assert.Equal(rowsAffected, result);
    }
}