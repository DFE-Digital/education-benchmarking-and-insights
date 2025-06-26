namespace Platform.Sql.QueryBuilders;

public class ActiveBannersQuery() : PlatformQuery(Sql)
{
    // see below for commentary on OPENJSON with CROSS APPLY when extracting from JSON arrays:
    // https://learn.microsoft.com/en-us/sql/t-sql/functions/openjson-transact-sql#example-3---join-rows-with-json-data-stored-in-table-cells-using-cross-apply
    private const string Sql = "SELECT * FROM VW_ActiveBanners CROSS APPLY OPENJSON([Target], '$') /**where**/ /**orderby**/";
}