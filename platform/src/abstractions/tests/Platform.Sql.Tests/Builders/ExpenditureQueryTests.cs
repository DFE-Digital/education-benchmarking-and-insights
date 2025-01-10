using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ExpenditureSchoolDefaultCurrentQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit " },
    };
    
    private static ExpenditureSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolCustomQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolCustomActual " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolCustomPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolCustomPercentIncome " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolCustomPerUnit " },
    };
    
    private static ExpenditureSchoolCustomQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultComparatorAvgQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentIncome " }
    };
    
    private static ExpenditureSchoolDefaultComparatorAvgQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultNationalAveQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAveActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentIncome " }
    };
    
    private static ExpenditureSchoolDefaultNationalAveQuery Create(string dimension) => new(dimension);
}

public class ExpenditureTrustDefaultCurrentQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome " }
    };
    
    private static ExpenditureTrustDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class ExpenditureTrustDefaultQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureTrustDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureTrustDefaultPerUnit " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureTrustDefaultPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureTrustDefaultPercentIncome " }
    };
    
    private static ExpenditureTrustDefaultQuery Create(string dimension) => new(dimension);
}

public class ExpenditureSchoolDefaultQueryTests
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
        { "Actuals", "SELECT * FROM VW_ExpenditureSchoolDefaultActual " },
        { "PerUnit", "SELECT * FROM VW_ExpenditureSchoolDefaultPercentExpenditure " },
        { "PercentExpenditure", "SELECT * FROM VW_ExpenditureSchoolDefaultPercentIncome " },
        { "PercentIncome", "SELECT * FROM VW_ExpenditureSchoolDefaultPerUnit " }
    };
    
    private static ExpenditureSchoolDefaultQuery Create(string dimension) => new(dimension);
}