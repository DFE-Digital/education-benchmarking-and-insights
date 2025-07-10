using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Platform.ApiTests.TestDataHelpers;

public static class TestDataProvider
{
    public static JArray GetArrayData(string fileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"Platform.ApiTests.Data.{fileName}";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
            throw new InvalidOperationException($"Embedded resource '{resourceName}' not found.");

        using var reader = new StreamReader(stream);
        var jsonString = reader.ReadToEnd();

        return JArray.Parse(jsonString);
    }
}
