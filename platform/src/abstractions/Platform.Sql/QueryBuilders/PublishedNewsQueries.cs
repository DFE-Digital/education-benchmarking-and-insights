namespace Platform.Sql.QueryBuilders;

public class PublishedNewsQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_PublishedNews /**where**/ /**orderby**/";
}