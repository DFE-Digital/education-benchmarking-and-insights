namespace Platform.Sql.QueryBuilders;

public class PublishedNewsQuery : PlatformQuery
{
    public PublishedNewsQuery(params string[] fields) : base(GetSql(fields))
    {
        foreach (var field in fields)
        {
            Select(field);
        }
    }

    private static string GetSql(string[] fields)
    {
        var select = fields.Length == 0 ? "*" : "/**select**/";
        return $"SELECT {select} FROM VW_PublishedNews /**where**/ /**orderby**/";
    }
}