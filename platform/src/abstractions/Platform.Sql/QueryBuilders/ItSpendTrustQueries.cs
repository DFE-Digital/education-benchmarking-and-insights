using Platform.Domain;

namespace Platform.Sql.QueryBuilders;

public class ItSpendTrustCurrentPreviousYearQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_ItSpendTrustCurrentPreviousYearActual /**where**/";
}

public class ItSpendTrustCurrentAllYearsQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_ItSpendTrustCurrentAllYearsActual /**where**/";
}