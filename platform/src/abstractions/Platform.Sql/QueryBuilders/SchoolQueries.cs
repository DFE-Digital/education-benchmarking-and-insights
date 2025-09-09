namespace Platform.Sql.QueryBuilders;

public class SchoolQuery : PlatformQuery
{
    private const string SqlAll = "SELECT * FROM School /**where**/";
    private const string SqlFields = "SELECT /**select**/ FROM School /**where**/";

    public SchoolQuery(params string[] fields) : base(GetSql(fields))
    {
        foreach (var field in fields)
        {
            Select(field);
        }
    }

    private static string GetSql(string[] fields) => fields.Length == 0 ? SqlAll : SqlFields;
}