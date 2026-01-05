using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Platform.ApiTests.TestDataHelpers;

public static class TestDataProvider
{
    public static JArray GetJsonArrayData(string fileName, params string[] folders)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = BuildResourceName(fileName, folders);

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var jsonString = reader.ReadToEnd();

        return JArray.Parse(jsonString);
    }

    public static JObject GetJsonObjectData(string fileName, params string[] folders)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = BuildResourceName(fileName, folders);

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        }

        using var reader = new StreamReader(stream, Encoding.UTF8);
        var jsonString = reader.ReadToEnd();

        return JObject.Parse(jsonString);
    }

    public static XDocument GetXmlData(string fileName, params string[] folders)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = BuildResourceName(fileName, folders);

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");
        }

        return XDocument.Load(stream);
    }

    private static string BuildResourceName(string fileName, params string[] folders)
    {
        return folders.Length > 0
            ? $"Platform.ApiTests.Data.{string.Join(".", folders)}.{fileName}"
            : $"Platform.ApiTests.Data.{fileName}";
    }
}