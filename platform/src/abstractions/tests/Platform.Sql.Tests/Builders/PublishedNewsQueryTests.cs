using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class PublishedNewsQueryTests
{
    [Theory]
    [InlineData(new string[]
    {
    }, "SELECT * FROM VW_PublishedNews")]
    [InlineData(new[]
    {
        "Title"
    }, "SELECT Title\n FROM VW_PublishedNews")]
    public void ShouldReturnSql(string[] fields, string expected)
    {
        var builder = Create(fields);
        Assert.Equal(expected, builder.QueryTemplate.RawSql.Trim());
    }

    private static PublishedNewsQuery Create(params string[] fields)
    {
        return new PublishedNewsQuery(fields);
    }
}