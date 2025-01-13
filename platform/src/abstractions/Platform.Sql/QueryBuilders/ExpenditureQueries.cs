using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class ExpenditureSchoolDefaultCurrentQuery(string dimension) : PlatformQuery(GetSql(dimension))
{
    private static string GetSql(string dimension)
    {
        return dimension switch
        {
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentActual /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPercentIncome /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultCurrentPerUnit /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureSchoolCustomActual /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolCustomPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolCustomPercentIncome /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureSchoolCustomPerUnit /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultComparatorAvgPercentIncome /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAveActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultNationalAvePercentIncome /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultCurrentPercentIncome /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureTrustDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureTrustDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureTrustDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureTrustDefaultPercentIncome /**where**/",
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
            Dimension.Cost.Actuals => "SELECT * FROM VW_ExpenditureSchoolDefaultActual /**where**/",
            Dimension.Cost.PerUnit => "SELECT * FROM VW_ExpenditureSchoolDefaultPerUnit /**where**/",
            Dimension.Cost.PercentExpenditure => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentExpenditure /**where**/",
            Dimension.Cost.PercentIncome => "SELECT * FROM VW_ExpenditureSchoolDefaultPercentIncome /**where**/",
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), "Unknown dimension")
        };
    }
}