namespace Platform.Sql.QueryBuilders;

public class SchoolRisksDefaultQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_LASchoolRisksDefault /**where**/";
}
