namespace Web.App.Domain.Content;

public record News
{
    public string? Title { get; set; }
    public string? Slug { get; set; }
    public string? Body { get; set; }
    public DateTimeOffset? Published { get; set; }
}