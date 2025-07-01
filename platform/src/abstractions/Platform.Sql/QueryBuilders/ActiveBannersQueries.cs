namespace Platform.Sql.QueryBuilders;

public class ActiveBannersQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_ActiveBanners /**where**/ /**orderby**/";
}