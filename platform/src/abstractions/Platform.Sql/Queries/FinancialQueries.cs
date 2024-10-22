using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetCurrentFinancial(params string[] fields)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            $"SELECT /**select**/ FROM Financial WHERE RunType = 'default' AND RunId = ({GetCurrentYear})");
        
        foreach (var field in fields)
        {
            builder.Select(field);
        }
        
        return template;
    }
}