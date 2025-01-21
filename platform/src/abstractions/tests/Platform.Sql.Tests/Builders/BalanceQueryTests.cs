using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class BalanceSchoolDefaultCurrentQueryTests
{
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

    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceSchoolDefaultCurrentActual " }
    };

    private static BalanceSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class BalanceTrustDefaultCurrentQueryTests
{
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

    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceTrustDefaultCurrentActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceTrustDefaultCurrentPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceTrustDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceTrustDefaultCurrentPercentIncome " },
    };

    private static BalanceTrustDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class BalanceTrustDefaultQueryTests
{
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

    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceTrustDefaultPercentIncome " }
    };

    private static BalanceTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class BalanceSchoolDefaultQueryTests
{
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

    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceSchoolDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceSchoolDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceSchoolDefaultPercentIncome " }
    };

    private static BalanceSchoolDefaultQuery Create(string dimension) => new(dimension);
}