using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class SchoolsFinanceSummaryDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT /**select**/ FROM VW_SchoolsFinancialSummaryDefaultCurrentActual /**where**/ /**orderby**/",
            Dimensions.Finance.PerUnit => "SELECT /**select**/ FROM VW_SchoolsFinancialSummaryDefaultCurrentPerUnit /**where**/ /**orderby**/",
            Dimensions.Finance.PercentExpenditure => "SELECT /**select**/ FROM VW_SchoolsFinancialSummaryDefaultCurrentPercentExpenditure /**where**/ /**orderby**/",
            Dimensions.Finance.PercentIncome => "SELECT /**select**/ FROM VW_SchoolsFinancialSummaryDefaultCurrentPercentIncome /**where**/ /**orderby**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}