using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class CensusSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal /**where**/",
            Dimension.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte /**where**/",
            Dimension.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce /**where**/",
            Dimension.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class CensusSchoolDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultTotal /**where**/",
            Dimension.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultHeadcountPerFte /**where**/",
            Dimension.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultPercentWorkforce /**where**/",
            Dimension.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultPupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class CensusSchoolCustomQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Census.Total => "SELECT * FROM VW_CensusSchoolCustomTotal /**where**/",
            Dimension.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolCustomHeadcountPerFte /**where**/",
            Dimension.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolCustomPercentWorkforce /**where**/",
            Dimension.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolCustomPupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class CensusSchoolDefaultNationalAveQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultNationalAveTotal /**where**/",
            Dimension.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultNationalAveHeadcountPerFte /**where**/",
            Dimension.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePercentWorkforce /**where**/",
            Dimension.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class CensusSchoolDefaultComparatorAveQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveTotal /**where**/",
            Dimension.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveHeadcountPerFte /**where**/",
            Dimension.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePercentWorkforce /**where**/",
            Dimension.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}