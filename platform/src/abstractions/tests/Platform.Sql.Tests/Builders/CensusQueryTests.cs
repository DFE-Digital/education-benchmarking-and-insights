using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class CensusSchoolDefaultCurrentQueryTests
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
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole " }
    };

    private static CensusSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultQueryTests
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
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultPupilsPerStaffRole " }
    };

    private static CensusSchoolDefaultQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolCustomQueryTests
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
        { "Total", "SELECT * FROM VW_CensusSchoolCustomTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolCustomHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolCustomPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolCustomPupilsPerStaffRole " }
    };

    private static CensusSchoolCustomQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultNationalAveQueryTests
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
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultNationalAveTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultNationalAveHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultNationalAvePercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole " }
    };

    private static CensusSchoolDefaultNationalAveQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultComparatorAveQueryTests
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
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultComparatorAveTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultComparatorAveHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole " }
    };

    private static CensusSchoolDefaultComparatorAveQuery Create(string dimension) => new(dimension);
}