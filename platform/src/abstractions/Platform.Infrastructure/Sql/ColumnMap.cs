using System.Diagnostics.CodeAnalysis;

namespace Platform.Infrastructure.Sql;

[ExcludeFromCodeCoverage]
public class ColumnMap
{
    private readonly Dictionary<string, string> _forward = new();
    private readonly Dictionary<string, string> _reverse = new();

    public void Add(string t1, string t2)
    {
        _forward.Add(t1, t2);
        _reverse.Add(t2, t1);
    }

    public string this[string index] => _forward.TryGetValue(index, out var item)
        ? item
        : _reverse.GetValueOrDefault(index, index);
}