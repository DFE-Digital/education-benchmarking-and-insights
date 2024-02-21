namespace EducationBenchmarking.Web.ViewModels;

public class TrackedAnchorViewModel(string eventId, object href, string content, string? hiddenContent, string? target, string[]? rel, params string[] classes)
{
    public string EventId => eventId;
    public object Href => href;
    public string Classes => string.Join(" ", classes);
    public string Content => content;
    public string? Target => target;
    public string? Rel => rel is null || rel.Length == 0 ? null : string.Join(" ", rel);
    public string? HiddenContent => hiddenContent;
}