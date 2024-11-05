namespace Web.App.ViewModels.Components;

public class RagPositionViewModel(IEnumerable<(string urn, decimal value)> values, int itemWidth, int height, decimal itemSpacing)
{
    public IEnumerable<(string urn, decimal value)> Values => values
        .OrderBy(v => v.value)
        .ThenBy(v => v.urn);
    public int ItemWidth => itemWidth;
    public int Height => height;
    public decimal ItemSpacing => itemSpacing;
    public string? Highlight { get; init; }
    public string? Id { get; init; }
    public string? Title { get; init; }
}