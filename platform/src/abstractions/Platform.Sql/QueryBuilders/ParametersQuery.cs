namespace Platform.Sql.QueryBuilders;

public class ParametersQuery : PlatformQuery
{
    private const string Sql = "SELECT Value FROM Parameters /**where**/";

    public ParametersQuery(string name) : base(Sql)
    {
        WhereNameEqual(name);
    }
}