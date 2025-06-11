using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Files.Models;
using Platform.Api.Content.Features.Files.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Content.Tests.Files.Services;

public class WhenFilesServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly FilesService _service;

    public WhenFilesServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new FilesService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsync()
    {
        var types = _fixture.CreateMany<string>().ToArray();
        var results = _fixture.Build<FileModel>().CreateMany().ToArray();
        string? actualSql = null;
        Dictionary<string, object>? actualParams = null;

        _connection
            .Setup(c => c.QueryAsync<FileModel>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
                actualParams = query.QueryTemplate.Parameters?.GetTemplateParameters("Types");
            })
            .ReturnsAsync(results);

        var actual = await _service.GetActiveFilesByType(CancellationToken.None, types);

        Assert.Equal(results, actual);
        Assert.Equal("SELECT Type , Label , FileName\n FROM VW_ActiveFiles WHERE Type IN @Types", actualSql);
        Assert.Equal(new Dictionary<string, object>
        {
            { "Types", types }
        }, actualParams);
    }
}