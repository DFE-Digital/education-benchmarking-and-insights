namespace Web.E2ETests.Assist;

public class DynamicInstanceComparisonException(IList<string> diffs) : Exception("There were some difference between the table and the instance")
{
    public IList<string> Differences { get; private set; } = diffs;
}