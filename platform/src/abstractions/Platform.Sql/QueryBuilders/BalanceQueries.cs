using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class BalanceSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultCurrentActual /**where**/",
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
            Dimensions.Finance.Actuals => "SELECT * FROM VW_BalanceTrustDefaultCurrentActual /**where**/",
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
            Dimensions.Finance.Actuals => "SELECT * FROM VW_BalanceTrustDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_BalanceTrustDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_BalanceTrustDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_BalanceTrustDefaultPercentIncome /**where**/",
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
            Dimensions.Finance.Actuals => "SELECT * FROM VW_BalanceSchoolDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_BalanceSchoolDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_BalanceSchoolDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_BalanceSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}