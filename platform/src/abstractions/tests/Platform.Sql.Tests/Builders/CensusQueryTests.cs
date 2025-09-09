using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class CensusSchoolDefaultCurrentQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole " }
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

    private static CensusSchoolDefaultCurrentQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultPupilsPerStaffRole " }
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

    private static CensusSchoolDefaultQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolCustomQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Total", "SELECT * FROM VW_CensusSchoolCustomTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolCustomHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolCustomPercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolCustomPupilsPerStaffRole " }
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

    private static CensusSchoolCustomQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultNationalAveQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultNationalAveTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultNationalAveHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultNationalAvePercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole " }
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

    private static CensusSchoolDefaultNationalAveQuery Create(string dimension) => new(dimension);
}

public class CensusSchoolDefaultComparatorAveQueryTests
{
    public static TheoryData<string, string> Data => new()
    {
        { "Total", "SELECT * FROM VW_CensusSchoolDefaultComparatorAveTotal " },
        { "HeadcountPerFte", "SELECT * FROM VW_CensusSchoolDefaultComparatorAveHeadcountPerFte " },
        { "PercentWorkforce", "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePercentWorkforce " },
        { "PupilsPerStaffRole", "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole " }
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

    private static CensusSchoolDefaultComparatorAveQuery Create(string dimension) => new(dimension);
}