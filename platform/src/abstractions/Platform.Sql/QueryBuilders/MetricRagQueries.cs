namespace Platform.Sql.QueryBuilders;

public class SchoolMetricRagQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM SchoolMetricRAG /**where**/";
}

public class MetricRagSummaryQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_MetricRagSummaryExcludingOtherDefaultCurrent /**where**/";
}