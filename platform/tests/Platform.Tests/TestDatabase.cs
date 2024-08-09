using System.Reflection;

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
}