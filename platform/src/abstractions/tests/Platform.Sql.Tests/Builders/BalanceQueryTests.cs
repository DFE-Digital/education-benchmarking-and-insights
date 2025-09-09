using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class BalanceSchoolDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceSchoolDefaultCurrentActual " }
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

    private static BalanceSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class BalanceTrustDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceTrustDefaultCurrentActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceTrustDefaultCurrentPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceTrustDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceTrustDefaultCurrentPercentIncome " }
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

    private static BalanceTrustDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class BalanceTrustDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceTrustDefaultPercentIncome " }
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

    private static BalanceTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class BalanceSchoolDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_BalanceSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_BalanceSchoolDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_BalanceSchoolDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_BalanceSchoolDefaultPercentIncome " }
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

    private static BalanceSchoolDefaultQuery Create(string dimension) => new(dimension);
}