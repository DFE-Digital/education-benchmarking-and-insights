namespace Web.App.Domain.Content;

public record CommercialResourceCategorised : CommercialResourceLink
{
    public CategoryCollection Category { get; set; } = new();
    public CategoryCollection SubCategory { get; set; } = new();
}

public record CategoryCollection
{
    public string[] Items { get; set; } = [];
}

public record CommercialResourceLink
{
    public string? Title { get; set; }
    public string? Url { get; set; }
}