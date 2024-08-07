using System.Text.Json;
namespace Platform.Tests;

public abstract class TestDataReader
{
    public static T ReadTestDataFromFile<T>(string filename, Type testType)
    {
        var paths = testType.Namespace?.Split(".").Skip(2).ToList()!;
        paths.Insert(0, Environment.CurrentDirectory);
        paths.Add(filename);

        // source file must be set to `CopyToOutputDirectory`
        var json = File.ReadAllText(Path.Combine(paths.ToArray()));
        return JsonSerializer.Deserialize<T>(json)!;
    }
}