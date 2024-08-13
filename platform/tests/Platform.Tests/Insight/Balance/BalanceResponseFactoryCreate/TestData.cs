using System.Collections;
using Platform.Api.Insight.Balance;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Tests.Insight.Balance.BalanceResponseFactoryCreate;

internal class TotalBalanceTestData<T> : IEnumerable<object[]> where T : BalanceBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolBalanceResponseTestData<T>.GetTestDataFromFile("TotalBalanceTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record SchoolBalanceResponseTestData<T> where T : BalanceBaseResponse, new()
{
    public string? Dimension { get; set; }
    public bool ExcludeCentralServices { get; set; }
    public T Expected { get; set; } = new();

    public static IEnumerator<object[]> GetTestDataFromFile(string filename)
    {
        var items = TestDataReader.ReadTestDataFromFile<SchoolBalanceResponseTestData<T>[]>(filename, typeof(SchoolBalanceResponseTestData<T>));
        foreach (var item in items)
        {
            yield return [item.Dimension!, item.ExcludeCentralServices, item.Expected];
        }
    }
}