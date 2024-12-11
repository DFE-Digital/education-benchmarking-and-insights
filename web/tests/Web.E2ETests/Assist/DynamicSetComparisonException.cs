namespace Web.E2ETests.Assist;

public class DynamicSetComparisonException(string message) : Exception(message)
{
    public DynamicSetComparisonException(string message, IList<string> differences)
        : this($"{message}:{Environment.NewLine}{string.Join(Environment.NewLine, differences)}")
    {
        Differences = differences;
    }

    public IList<string> Differences { get; private set; } = new List<string>();
}