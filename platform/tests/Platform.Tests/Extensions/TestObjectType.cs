namespace Platform.Tests.Extensions;

public class TestObjectType(string testProp) : IEqualityComparer<TestObjectType>
{
    public string TestProp { get; } = testProp;

    public bool Equals(TestObjectType? x, TestObjectType? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }
        if (ReferenceEquals(x, null))
        {
            return false;
        }
        if (ReferenceEquals(y, null))
        {
            return false;
        }
        if (x.GetType() != y.GetType())
        {
            return false;
        }
        return x.TestProp == y.TestProp;
    }

    public int GetHashCode(TestObjectType obj) => obj.TestProp.GetHashCode();
}