using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class BalanceSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultCurrentActual /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class BalanceTrustDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class BalanceTrustDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_BalanceTrustDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_BalanceTrustDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_BalanceTrustDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_BalanceTrustDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class BalanceSchoolDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_BalanceSchoolDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_BalanceSchoolDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_BalanceSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}