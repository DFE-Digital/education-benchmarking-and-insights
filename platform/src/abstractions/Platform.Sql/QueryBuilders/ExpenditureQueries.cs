using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class ExpenditureSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureSchoolCustomQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureSchoolCustomActual /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolCustomPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolCustomPercentIncome /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureSchoolCustomPerUnit /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureSchoolDefaultComparatorAvgQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureSchoolDefaultNationalAveQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAveActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureTrustDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureTrustDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}

public class ExpenditureSchoolDefaultQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimensions.Finance.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultActual /**where**/",
            Dimensions.Finance.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultPerUnit /**where**/",
            Dimensions.Finance.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentExpenditure /**where**/",
            Dimensions.Finance.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}