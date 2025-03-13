namespace Platform.Sql.QueryBuilders;

public class LocalAuthorityQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM LocalAuthority /**where**/ /**orderby**/";
}

public class LocalAuthorityStatisticalNeighbourQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM VW_LocalAuthorityStatisticalNeighbours /**where**/";
}