namespace Platform.ApiTests.Assist;

public class DynamicSetComparisonException(string message) : Exception(message)
{
    public DynamicSetComparisonException(string message, IList<string> differences) : this(message)
    {
        Differences = differences;
    }

    public IList<string> Differences { get; private set; } = new List<string>();
}