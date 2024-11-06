using System.Collections.ObjectModel;
using Web.App.Domain;
namespace Web.App.ViewModels.Components;

public class RagPositionViewModel(ReadOnlyDictionary<string, Category> values, int itemWidth, int height, decimal itemSpacing)
{
    public IEnumerable<(string urn, decimal value)> Values => values
        .OrderBy(v => v.Value.Value)
        .ThenBy(v => v.Key)
        .Select(v => (v.Key, v.Value.Value));
    public int ItemWidth => itemWidth;
    public int Height => height;
    public decimal ItemSpacing => itemSpacing;
    public string? Highlight { get; init; }
    public string? Id { get; init; }
    public string? Title { get; init; }
}