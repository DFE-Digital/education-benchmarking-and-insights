using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ActiveFilesQueryTests
{
    [Fact]
    public void ShouldReturnSqlWithoutFields()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM VW_ActiveFiles ", builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldReturnSqlWithFields()
    {
        var builder = Create("Type", "Label", "FileName");
        Assert.Equal("SELECT Type , Label , FileName\n FROM VW_ActiveFiles ", builder.QueryTemplate.RawSql);
    }

    private static ActiveFilesQuery Create(params string[] fields) => new(fields);
}