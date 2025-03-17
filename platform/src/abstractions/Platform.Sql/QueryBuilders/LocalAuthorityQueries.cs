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
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class LocalAuthorityFinancialDefaultCurrentRankingQuery : PlatformQuery
{
    public LocalAuthorityFinancialDefaultCurrentRankingQuery(string ranking, string sort) : base(GetSql(ranking))
    {
        Select("LaCode AS Code");
        Select("Name");
        Select("Value");
        Select(sort.Equals(Ranking.Sort.Desc, StringComparison.OrdinalIgnoreCase)
            ? "RANK() OVER (ORDER BY Value DESC) AS [Rank]"
            : "RANK() OVER (ORDER BY Value) AS [Rank]");
    }

    private static string GetSql(string ranking)
    {
        return ranking switch
        {
            Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfBudget => "SELECT /**select**/ FROM VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(ranking), "Unknown ranking")
        };
    }
}