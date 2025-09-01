using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class PublishedNewsQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM VW_PublishedNews  ", builder.QueryTemplate.RawSql);
    }

    private static PublishedNewsQuery Create()
    {
        return new PublishedNewsQuery();
    }
}