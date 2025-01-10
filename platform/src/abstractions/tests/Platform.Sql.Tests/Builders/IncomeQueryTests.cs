using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class IncomeSchoolDefaultCurrentQueryTests
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
    
    public static TheoryData<string, string> Data => new () 
    {
        { "Actuals", "SELECT * FROM VW_IncomeSchoolDefaultCurrentActual " }
    };
    
    private static IncomeSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class IncomeTrustDefaultQueryTests
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
    
    public static TheoryData<string, string> Data => new () 
    {
        { "Actuals", "SELECT * FROM VW_IncomeTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_IncomeTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_IncomeTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_IncomeTrustDefaultPercentIncome " },
    };
    
    private static IncomeTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class IncomeSchoolDefaultQueryTest
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
    
    public static TheoryData<string, string> Data => new () 
    {
        { "Actuals", "SELECT * FROM VW_IncomeSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_IncomeSchoolDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_IncomeSchoolDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_IncomeSchoolDefaultPercentIncome " }
    };
    
    private static IncomeSchoolDefaultQuery Create(string dimension) => new(dimension);
}