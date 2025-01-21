namespace Platform.Sql.QueryBuilders;

public class ActiveFilesQuery : PlatformQuery
{
    private const string SqlAll = "SELECT * FROM VW_ActiveFiles /**where**/";
    private const string SqlFields = "SELECT /**select**/ FROM VW_ActiveFiles /**where**/";

    public ActiveFilesQuery(params string[] fields) : base(GetSql(fields))
    {
        foreach (var field in fields)
        {
            Select(field);
        }
    }

    private static string GetSql(string[] fields) => fields.Length == 0 ? SqlAll : SqlFields;
}