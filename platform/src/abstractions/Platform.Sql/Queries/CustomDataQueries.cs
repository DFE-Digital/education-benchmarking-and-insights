using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetCustomData(string? urn, string? id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM CustomDataSchool WHERE URN = @URN AND Id = @Id",
            new { URN = urn, Id = id });

        return template;
    }
}