using Dapper;

namespace Platform.Sql;

public static partial class Queries
{
    public static SqlBuilder.Template GetMetricRag(string runId, string runType = "default",bool includeSubCategories = false, string[]? urns = null, string[]? categories = null, string[]? statuses =null)
    {
        var builder = new SqlBuilder();
        var template = builder.AddTemplate("SELECT * from MetricRAG /**where**/");
        builder.Where("RunType = @RunType AND RunId = @RunId",
            new
            {
                RunType = runType,
                RunId = runId
            });

        if (!includeSubCategories)
        {
            builder.Where("SubCategory = 'Total'");
        }
        
        if (urns?.Length != 0)
        {
            builder.Where("URN IN @URNS", new
            {
                URNS = urns
            });
        }
        
        if (categories?.Length != 0)
        {
            builder.Where("Category IN @categories", new
            {
                categories
            });
        }

        if (statuses?.Length != 0)
        {
            builder.Where("RAG IN @statuses", new
            {
                statuses
            });
        }
        
        return template;
    }
}