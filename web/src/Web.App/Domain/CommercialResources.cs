namespace Web.App.Domain;

public record CommercialResources
{
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
}

public static class CommercialResourcesBuilder
{
    public static IEnumerable<GroupedResources> GroupByValidCategory(IEnumerable<CommercialResources> resources) => resources
        .Where(r => Category.All.Contains(r.Category))
        .GroupBy(r => r.Category)
        .OrderBy(g => Lookups.CategoryOrderMap[g.Key ?? string.Empty])
        .Select(g => new GroupedResources
        {
            Category = g.Key,
            Sections = g
                .GroupBy(r => r.SubCategory)
                .OrderBy(sg => sg.Key)
                .Select(sg => new SectionGroup
                {
                    Section = sg.Key,
                    Links = sg
                        .OrderBy(r => r.Title)
                        .Select(r => new LinkItem { Title = r.Title, Url = r.Url })
                        .ToList()
                })
                .ToList()
        })
        .ToList();

    public static (string? Title, string? Url)? GetFindAFrameworkLink(IEnumerable<CommercialResources> resources) => resources
        .FirstOrDefault(r => r is { Category: CommercialResourcesConstants.AllFrameworksCategory, Title: not null, Url: not null }) is { } resource
        ? (resource.Title, resource.Url)
        : null;
}

public record GroupedResources
{
    public string? Category { get; init; }
    public List<SectionGroup> Sections { get; init; } = [];
}

public record SectionGroup
{
    public string? Section { get; init; }
    public List<LinkItem> Links { get; init; } = [];
}

public record LinkItem
{
    public string? Title { get; init; }
    public string? Url { get; init; }
}