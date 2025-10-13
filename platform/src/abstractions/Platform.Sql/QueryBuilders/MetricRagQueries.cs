namespace Platform.Sql.QueryBuilders;

public class SchoolMetricRagQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM SchoolMetricRAG /**where**/";
}