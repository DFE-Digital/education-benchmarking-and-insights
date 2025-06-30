namespace Web.App.Domain.Content;

public record Banner
{
    public string? Title { get; set; }
    public string? Heading { get; set; }
    public string? Body { get; set; }
}