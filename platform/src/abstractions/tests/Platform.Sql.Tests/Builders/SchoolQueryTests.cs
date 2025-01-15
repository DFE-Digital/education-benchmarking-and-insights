using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class SchoolQueryTests
{
    [Fact]
    public void ShouldReturnSqlWithoutFields()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM School ", builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldReturnSqlWithFields()
    {
        var builder = Create("field1", "field2", "field3");
        Assert.Equal("SELECT field1 , field2 , field3\n FROM School ", builder.QueryTemplate.RawSql);
    }


    private static SchoolQuery Create(params string[] fields) => new(fields);
}