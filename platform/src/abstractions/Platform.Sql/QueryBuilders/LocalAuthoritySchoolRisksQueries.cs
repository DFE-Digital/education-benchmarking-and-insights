namespace Platform.Sql.QueryBuilders;

public class LocalAuthoritySchoolRisksPagedQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_LASchoolRisksDefaultCurrent /**where**/ /**orderby**/ /**offset**/ /**fetch**/";
}

public class LocalAuthoritySchoolRisksCountQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT COUNT(*) FROM VW_LASchoolRisksDefaultCurrent /**where**/";
}
