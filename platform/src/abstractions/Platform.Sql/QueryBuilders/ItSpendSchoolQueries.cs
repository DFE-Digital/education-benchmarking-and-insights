using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class ItSpendSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ItSpendSchoolDefaultCurrentActual /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPercentIncome /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ItSpendSchoolDefaultCurrentPerUnit /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}