using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class IncomeSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultCurrentActual /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class IncomeTrustDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_IncomeTrustDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_IncomeTrustDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_IncomeTrustDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_IncomeTrustDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class IncomeSchoolDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_IncomeSchoolDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_IncomeSchoolDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_IncomeSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}