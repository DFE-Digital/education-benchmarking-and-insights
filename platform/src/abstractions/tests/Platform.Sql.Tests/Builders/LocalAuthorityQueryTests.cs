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

public class LocalAuthorityFinancialDefaultCurrentQueryTests
{
    public static TheoryData<string, string[], string> Data => new()
    {
        { "Actuals", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentActual " },
        { "Actuals", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultCurrentActual " },
        { "PerHead", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentPerPopulation " },
        { "PerHead", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultCurrentPerPopulation " }
    };

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

    private static LocalAuthorityFinancialDefaultCurrentQuery Create(string dimension, string[] fields) => new(dimension, fields);
}

public class LocalAuthorityFinancialDefaultQueryTests
{
    public static TheoryData<string, string[], string> Data => new()
    {
        { "Actuals", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultActual " },
        { "Actuals", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultActual " },
        { "PerHead", [], "SELECT * FROM VW_LocalAuthorityFinancialDefaultPerPopulation " },
        { "PerHead", ["field1", "field2"], "SELECT field1 , field2\n FROM VW_LocalAuthorityFinancialDefaultPerPopulation " }
    };

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

    private static LocalAuthorityFinancialDefaultQuery Create(string dimension, string[] fields) => new(dimension, fields);
}

public class LocalAuthorityEducationHealthCarePlansDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentActual " },
        { "Per1000", "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPopulation " },
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldReturnSql(string dimension, string expected)
    {
        var builder = Create(dimension);
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create("dimension"));
    }

    private static LocalAuthorityEducationHealthCarePlansDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class LocalAuthorityFinancialDefaultCurrentRankingQueryTests
{
    public static TheoryData<string, string?, string> Data => new()
    {
        { "SpendAsPercentageOfBudget", null, "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget " },
        { "SpendAsPercentageOfBudget", "desc", "SELECT Code , Name , Value , RANK() OVER (ORDER BY Value desc) AS [Rank]\n FROM VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget " },
        { "SpendAsPercentageOfBudget", "invalid", "SELECT * FROM VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget " }
    };

    [Theory]
    [MemberData(nameof(Data))]
    public void ShouldReturnSql(string ranking, string? sort, string expected)
    {
        var builder = Create(ranking, sort);
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => Create("ranking"));
    }

    private static LocalAuthorityFinancialDefaultCurrentRankingQuery Create(string ranking, string? sort = null) => new(ranking, sort);
}