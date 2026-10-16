namespace Web.App.Domain.Content;

public record File
{
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? FileName { get; set; }
    public string? RunId { get; init; }
    public decimal? FileSize { get; init; }
}
