using System.Collections;
using Platform.Api.Insight.Expenditure;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Tests.Insight.Expenditure.ExpenditureResponseFactoryCreate;

internal class TotalExpenditureTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("TotalExpenditureTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class TeachingTeachingSupportStaffTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("TeachingTeachingSupportStaffTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class NonEducationalSupportStaffTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("NonEducationalSupportStaffTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class EducationalSuppliesTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("EducationalSuppliesTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class EducationalIctTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("EducationalIctTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PremisesStaffServicesTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("PremisesStaffServicesTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class UtilitiesTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("UtilitiesTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class AdministrationSuppliesTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("AdministrationSuppliesTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class CateringStaffServicesTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("CateringStaffServicesTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class OtherCostsTestData<T> : IEnumerable<object[]> where T : ExpenditureBaseResponse, new()
{
    public IEnumerator<object[]> GetEnumerator() => SchoolExpenditureResponseTestData<T>.GetTestDataFromFile("OtherCostsTestData.json");
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record SchoolExpenditureResponseTestData<T> where T : ExpenditureBaseResponse, new()
{
    public string? Category { get; set; }
    public string? Dimension { get; set; }
    public bool ExcludeCentralServices { get; set; }
    public T Expected { get; set; } = new();

    public static IEnumerator<object[]> GetTestDataFromFile(string filename)
    {
        var items = TestDataReader.ReadTestDataFromFile<SchoolExpenditureResponseTestData<T>[]>(filename, typeof(SchoolExpenditureResponseTestData<T>));
        foreach (var item in items)
        {
            yield return [item.Category!, item.Dimension!, item.ExcludeCentralServices, item.Expected];
        }
    }
}