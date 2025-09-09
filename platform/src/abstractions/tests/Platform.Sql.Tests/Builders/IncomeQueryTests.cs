using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class IncomeSchoolDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_IncomeSchoolDefaultCurrentActual " }
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

    private static IncomeSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class IncomeTrustDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_IncomeTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_IncomeTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_IncomeTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_IncomeTrustDefaultPercentIncome " }
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

    private static IncomeTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class IncomeSchoolDefaultQueryTest
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_IncomeSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_IncomeSchoolDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_IncomeSchoolDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_IncomeSchoolDefaultPercentIncome " }
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

    private static IncomeSchoolDefaultQuery Create(string dimension) => new(dimension);
}