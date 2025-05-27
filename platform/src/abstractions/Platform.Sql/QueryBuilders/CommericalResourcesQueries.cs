namespace Platform.Sql.QueryBuilders;

public class CommercialResourcesQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_CommercialResources";
}