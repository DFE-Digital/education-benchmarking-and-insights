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

public class LocalAuthorityCurrentFinancialQueryTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldReturnSql(string dimension, string[] fields, string expected)
    {
        var builder = Create(dimension, fields);
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create("dimension", []));
    }

    public static TheoryData<string, string[], string> Data => new()
    {
        { "Actuals", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentActual " },
        { "Actuals", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultCurrentActual " },
        { "PerHead", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentPerPopulation " },
        { "PerHead", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultCurrentPerPopulation " },
    };

    private static LocalAuthorityCurrentFinancialQuery Create(string dimension, string[] fields) => new(dimension, fields);
}