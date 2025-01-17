using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class IncomeSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultCurrentActual /**where**/",
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
            Dimensions.Finance.Actuals => "SELECT * FROM VW_IncomeTrustDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_IncomeTrustDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_IncomeTrustDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_IncomeTrustDefaultPercentIncome /**where**/",
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
            Dimensions.Finance.Actuals => "SELECT * FROM VW_IncomeSchoolDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_IncomeSchoolDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_IncomeSchoolDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_IncomeSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}