using System.Collections;
using Platform.Api.Insight.Census;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Tests.Insight.Census.CensusResponseFactoryCreate;

internal class WorkforceTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("WorkforceTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class TeachersTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("TeachersTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class SeniorLeadershipTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("SeniorLeadershipTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class TeachingAssistantTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("TeachingAssistantTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class NonClassroomSupportStaffTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("NonClassroomSupportStaffTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class AuxiliaryStaffTestData<T> : IEnumerable<object[]> where T : CensusBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => CensusResponseTestData<T>.GetTestDataFromFile("AuxiliaryStaffTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record CensusResponseTestData<T> where T : CensusBaseResponse, new()
{
    public string? Category { get; set; }
    public string? Dimension { get; set; }
    public T Expected { get; set; } = new();

    public static IEnumerator<object[]> GetTestDataFromFile(string filename)
    {
        var items = TestDataReader.ReadTestDataFromFile<CensusResponseTestData<T>[]>(filename, typeof(CensusResponseTestData<T>));
        foreach (var item in items)
        {
            if (typeof(T) == typeof(CensusHistoryResponse))
            {
                yield return [item.Dimension!, item.Expected];
            }
            else
            {
                yield return [item.Category!, item.Dimension!, item.Expected];
            }
        }
    }
}