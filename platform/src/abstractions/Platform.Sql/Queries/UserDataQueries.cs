using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetUserDataById(string? id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "SELECT * FROM UserData WHERE Id = @Id",
            new { Id = id });

        return template;
    }

    public static SqlBuilder.Template UpdateUserDataSetStatusRemoved(string? id)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate(
            "UPDATE UserData SET Status = 'removed' WHERE Id = @Id",
            new { Id = id });

        return template;
    }

    public static SqlBuilder.Template GetUserDataByUserIds(string[] userIds, string? type = null, string? status = null,
        string? id = null, string? organisationId = null, string? organisationType = null)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * FROM UserData /**where**/");

        builder.Where(
            "UserId IN @userIds AND Status IN ('pending','complete')",
            new { userIds });

        if (!string.IsNullOrEmpty(organisationId))
        {
            builder.Where(
                "OrganisationId = @organisationId",
                new { organisationId });
        }

        if (!string.IsNullOrEmpty(organisationId))
        {
            builder.Where(
                "OrganisationType = @organisationType",
                new { organisationType });
        }

        if (!string.IsNullOrEmpty(type))
        {
            builder.Where(
                "Type = @type",
                new { type });
        }

        if (!string.IsNullOrEmpty(status))
        {
            builder.Where(
                "Status = @status",
                new { status });
        }

        if (!string.IsNullOrEmpty(id))
        {
            builder.Where(
                "Id = @id",
                new { id });
        }

        return template;
    }
}