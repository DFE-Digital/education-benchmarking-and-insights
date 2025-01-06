using System.Reflection;
using Microsoft.Data.SqlClient;
namespace Platform.Tests;

public static class TestDatabase
{
    public static Dictionary<string, object> GetDictionaryFromDynamicParameters(object? param, params string[] names)
    {
        var dictionary = new Dictionary<string, object>();
        if (param == null)
        {
            return dictionary;
        }

        var field = param.GetType().GetField("templates", BindingFlags.NonPublic | BindingFlags.Instance);
        if (field?.GetValue(param) is not List<dynamic> fieldValues)
        {
            return dictionary;
        }

        foreach (var fieldValue in fieldValues)
        {
            var type = fieldValue.GetType();
            foreach (var name in names)
            {
                var value = type.GetProperty(name)?.GetValue(fieldValue, null);
                if (value != null)
                {
                    dictionary.Add(name, value);
                }
            }
        }

        return dictionary;
    }

    public static SqlException MakeSqlException(string? message = null)
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