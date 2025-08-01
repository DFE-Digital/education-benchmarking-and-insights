using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class ItSpendSchoolDefaultCurrentQueryTests
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
        { "Actuals", "SELECT * FROM VW_ItSpendSchoolDefaultCurrentActual " },
        { "PercentExpenditure", "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPercentExpenditure " },
        { "PercentIncome", "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPercentIncome " },
        { "PerUnit", "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPerUnit " },
    };

    private static ItSpendSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}