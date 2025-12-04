using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class LocalAuthorityQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM LocalAuthority /**where**/ /**orderby**/";
}

public class LocalAuthorityStatisticalNeighbourQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_LocalAuthorityStatisticalNeighbours /**where**/";
}

public class LocalAuthorityFinancialDefaultCurrentQuery : PlatformQuery
{
    public LocalAuthorityFinancialDefaultCurrentQuery(string dimension, string[] fields) : base(GetSql(dimension, fields))
    {
        foreach (var field in fields)
        {
            Select(field);
        }
    }

    private static string GetSql(string dimension, string[] fields)
    {
        var select = fields.Length == 0 ? "*" : "/**select**/";
        return dimension switch
        {
            Dimensions.HighNeeds.Actuals => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultCurrentActual /**where**/",
            Dimensions.HighNeeds.PerHead => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultCurrentPerPopulation /**where**/",
            Dimensions.HighNeeds.PerPupil => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultCurrentPerPupil /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class LocalAuthorityFinancialDefaultQuery : PlatformQuery
{
    public LocalAuthorityFinancialDefaultQuery(string dimension, string[] fields) : base(GetSql(dimension, fields))
    {
        foreach (var field in fields)
        {
            Select(field);
        }
    }

    private static string GetSql(string dimension, string[] fields)
    {
        var select = fields.Length == 0 ? "*" : "/**select**/";
        return dimension switch
        {
            Dimensions.HighNeeds.Actuals => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultActual /**where**/",
            Dimensions.HighNeeds.PerHead => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultPerPopulation /**where**/",
            Dimensions.HighNeeds.PerPupil => $"SELECT {select} FROM VW_LocalAuthorityFinancialDefaultPerPupil /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class LocalAuthorityEducationHealthCarePlansDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.EducationHealthCarePlans.Actuals => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentActual /**where**/",
            Dimensions.EducationHealthCarePlans.Per1000 => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPopulation /**where**/",
            Dimensions.EducationHealthCarePlans.Per1000Pupil => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPupil /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class LocalAuthorityEducationHealthCarePlansDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.EducationHealthCarePlans.Actuals => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultActual /**where**/",
            Dimensions.EducationHealthCarePlans.Per1000 => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation /**where**/",
            Dimensions.EducationHealthCarePlans.Per1000Pupil => "SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultPerPupil /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}