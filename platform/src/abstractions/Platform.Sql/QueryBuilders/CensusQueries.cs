using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class CensusSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultCurrentTotal /**where**/",
            Dimensions.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultCurrentHeadcountPerFte /**where**/",
            Dimensions.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultCurrentPercentWorkforce /**where**/",
            Dimensions.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultCurrentPupilsPerStaffRole /**where**/",
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
            Dimensions.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultTotal /**where**/",
            Dimensions.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultHeadcountPerFte /**where**/",
            Dimensions.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultPercentWorkforce /**where**/",
            Dimensions.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultPupilsPerStaffRole /**where**/",
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
            Dimensions.Census.Total => "SELECT * FROM VW_CensusSchoolCustomTotal /**where**/",
            Dimensions.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolCustomHeadcountPerFte /**where**/",
            Dimensions.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolCustomPercentWorkforce /**where**/",
            Dimensions.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolCustomPupilsPerStaffRole /**where**/",
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
            Dimensions.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultNationalAveTotal /**where**/",
            Dimensions.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultNationalAveHeadcountPerFte /**where**/",
            Dimensions.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePercentWorkforce /**where**/",
            Dimensions.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultNationalAvePupilsPerStaffRole /**where**/",
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
            Dimensions.Census.Total => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveTotal /**where**/",
            Dimensions.Census.HeadcountPerFte => "SELECT * FROM VW_CensusSchoolDefaultComparatorAveHeadcountPerFte /**where**/",
            Dimensions.Census.PercentWorkforce => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePercentWorkforce /**where**/",
            Dimensions.Census.PupilsPerStaffRole => "SELECT * FROM VW_CensusSchoolDefaultComparatorAvePupilsPerStaffRole /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}