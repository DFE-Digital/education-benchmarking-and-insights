namespace Platform.Sql.QueryBuilders;

public class TrustQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM Trust /**where**/";
}

public class TrustCharacteristicsQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_TrustCharacteristics /**where**/";
}