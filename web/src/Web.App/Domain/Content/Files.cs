namespace Web.App.Domain;

public record File
{
    public string? Type { get; set; }
    public string? Label { get; set; }
    public string? FileName { get; set; }
}