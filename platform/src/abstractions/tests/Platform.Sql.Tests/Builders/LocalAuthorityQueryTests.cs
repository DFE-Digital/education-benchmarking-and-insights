using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class LocalAuthorityQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM LocalAuthority  ", builder.QueryTemplate.RawSql);
    }

    private static LocalAuthorityQuery Create() => new();
}