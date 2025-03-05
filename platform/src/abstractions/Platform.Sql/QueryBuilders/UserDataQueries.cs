namespace Platform.Sql.QueryBuilders;

public class UserDataQuery() : PlatformQuery(Sql)
{
    private const string Sql = "SELECT * FROM UserData /**where**/";
}