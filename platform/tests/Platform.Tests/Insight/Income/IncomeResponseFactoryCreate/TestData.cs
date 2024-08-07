using System.Collections;
using Platform.Api.Insight.Income;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Tests.Insight.Income.IncomeResponseFactoryCreate;

internal class TotalIncomeTestData<T> : IEnumerable<object[]> where T : IncomeBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolIncomeResponseTestData<T>.GetTestDataFromFile("TotalIncomeTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class GrantFundingTestData<T> : IEnumerable<object[]> where T : IncomeBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolIncomeResponseTestData<T>.GetTestDataFromFile("GrantFundingTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class SelfGeneratedTestData<T> : IEnumerable<object[]> where T : IncomeBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolIncomeResponseTestData<T>.GetTestDataFromFile("SelfGeneratedTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class DirectRevenueFinancingTestData<T> : IEnumerable<object[]> where T : IncomeBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolIncomeResponseTestData<T>.GetTestDataFromFile("DirectRevenueFinancingTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record SchoolIncomeResponseTestData<T> where T : IncomeBaseResponse, new()
{
    public string? Category { get; set; }
    public string? Dimension { get; set; }
    public bool ExcludeCentralServices { get; set; }
    public T Expected { get; set; } = new();

    public static IEnumerator<object[]> GetTestDataFromFile(string filename)
    {
        var items = TestDataReader.ReadTestDataFromFile<SchoolIncomeResponseTestData<T>[]>(filename, typeof(SchoolIncomeResponseTestData<T>));
        foreach (var item in items)
        {
            yield return [item.Category!, item.Dimension!, item.ExcludeCentralServices, item.Expected];
        }
    }
}