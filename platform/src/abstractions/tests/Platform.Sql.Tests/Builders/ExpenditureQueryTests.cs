using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ExpenditureSchoolDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit " }
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

    private static ExpenditureSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolCustomQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolCustomActual " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolCustomPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolCustomPercentIncome " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolCustomPerUnit " }
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

    private static ExpenditureSchoolCustomQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultComparatorAvgQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentIncome " }
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

    private static ExpenditureSchoolDefaultComparatorAvgQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultNationalAveQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAveActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentIncome " }
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

    private static ExpenditureSchoolDefaultNationalAveQuery Create(string dimension) => new(dimension);
}

public class ExpenditureTrustDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome " }
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

    private static ExpenditureTrustDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class ExpenditureTrustDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureTrustDefaultPercentIncome " }
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

    private static ExpenditureTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultPercentIncome " }
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

    private static ExpenditureSchoolDefaultQuery Create(string dimension) => new(dimension);
}