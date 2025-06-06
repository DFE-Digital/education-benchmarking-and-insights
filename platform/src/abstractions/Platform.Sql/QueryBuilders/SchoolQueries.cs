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

    private static string GetSql(string[] fields)
    {
        return fields.Length == 0 ? SqlAll : SqlFields;
    }
}

public class SchoolStatusQuery() : PlatformQuery(SqlAll)
{
    private const string SqlAll = "SELECT * FROM VW_SchoolStatus /**where**/";
}