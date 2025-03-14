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

public class LocalAuthorityCurrentFinancialQuery : PlatformQuery
{
    public LocalAuthorityCurrentFinancialQuery(string dimension, string[] fields) : base(GetSql(dimension, fields))
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