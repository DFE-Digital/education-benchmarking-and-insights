using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Dapper;

namespace Platform.Sql;

[ExcludeFromCodeCoverage]
public static class DynamicParametersExtensions
{
    public static Dictionary<string, object> GetTemplateParameters(this object parameters, params string[] names)
    {
        if (parameters is DynamicParameters dynamicParameters)
        {
            return GetTemplateParameters(dynamicParameters, names);
        }

        return new Dictionary<string, object>();
    }

    private static Dictionary<string, object> GetTemplateParameters(this DynamicParameters parameters, params string[] names)
    {
        var field = parameters.GetType().GetField("templates", BindingFlags.NonPublic | BindingFlags.Instance);
        if (field?.GetValue(parameters) is not List<dynamic> fieldValues)
        {
            return new Dictionary<string, object>();
        }

        var dictionary = new Dictionary<string, object>();
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
}