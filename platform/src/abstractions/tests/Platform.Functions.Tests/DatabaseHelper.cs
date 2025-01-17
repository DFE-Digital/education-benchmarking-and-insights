using System.Reflection;
using Microsoft.Data.SqlClient;

namespace Platform.Functions.Tests;

public static class DatabaseHelper
{
    public static SqlException SimulateSqlException(string? message = null)
    {
        SqlException? exception = null;
        try
        {
            var conn = new SqlConnection("Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1");
            conn.Open();
        }
        catch (SqlException ex)
        {
            exception = ex;

            if (!string.IsNullOrWhiteSpace(message))
            {
                var objType = exception.GetType();
                var fieldInfo = objType.GetField("_message", BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo?.SetValue(exception, message);
            }
        }

        return exception!;
    }
}