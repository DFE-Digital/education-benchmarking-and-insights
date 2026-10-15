using System.Data;
using Moq;
using Platform.Orchestrator.Sql;
using Platform.Sql;
using Xunit;

namespace Platform.Orchestrator.Tests.Sql;

public class WhenPipelineDbWritesLocalAuthorityRisksFileDetails
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly PipelineDb _db;
    private readonly Mock<IDbTransaction> _transaction;

    public WhenPipelineDbWritesLocalAuthorityRisksFileDetails()
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
    public async Task ShouldExecuteSqlWithParametersAndCommitTransaction()
    {
        const int expectedRowsAffected = 1;
        var fileRecord = new FileRecord
        {
            Type = "laa-risk-scores",
            Label = "Download detailed risk data for all schools",
            FileName = "laa_risk_scores_download.csv_2023_2024",
            ValidFrom = DateTimeOffset.UtcNow,
            ValidTo = DateTimeOffset.UtcNow.AddYears(1),
            RunId = "2024",
            Filesize = 2.4m
        };

        _connection
            .Setup(c => c.ExecuteAsync(
                It.IsAny<string>(),
                It.Is<object>(p => MatchParameters(p, fileRecord)),
                _transaction.Object))
            .ReturnsAsync(expectedRowsAffected);

        var result = await _db.WriteLocalAuthorityRisksFileDetails(fileRecord);

        Assert.Equal(expectedRowsAffected, result);
        _transaction.Verify(t => t.Commit(), Times.Once);
    }

    private static bool MatchParameters(object parameters, FileRecord expected)
    {
        var type = parameters.GetType();

        return Equals(type.GetProperty("Type")?.GetValue(parameters), expected.Type)
            && Equals(type.GetProperty("Label")?.GetValue(parameters), expected.Label)
            && Equals(type.GetProperty("FileName")?.GetValue(parameters), expected.FileName)
            && Equals(type.GetProperty("ValidFrom")?.GetValue(parameters), expected.ValidFrom)
            && Equals(type.GetProperty("ValidTo")?.GetValue(parameters), expected.ValidTo)
            && Equals(type.GetProperty("RunId")?.GetValue(parameters), expected.RunId)
            && Equals(type.GetProperty("Filesize")?.GetValue(parameters), expected.Filesize);
    }
}
