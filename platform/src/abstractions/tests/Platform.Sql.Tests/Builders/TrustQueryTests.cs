using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class TrustQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM Trust ", builder.QueryTemplate.RawSql);
    }


    private static TrustQuery Create() => new();
}

public class TrustCharacteristicsQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = Create();
        Assert.Equal("SELECT * FROM VW_TrustCharacteristics ", builder.QueryTemplate.RawSql);
    }


    private static TrustCharacteristicsQuery Create() => new();
}