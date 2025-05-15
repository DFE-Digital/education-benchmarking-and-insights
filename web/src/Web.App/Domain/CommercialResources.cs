namespace Web.App.Domain;

public record CommercialResources
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public string[] Category { get; set; } = [];
    public string[] SubCategory { get; set; } = [];
}

public static class CommercialResourcesBuilder
{
    public static IEnumerable<GroupedResources> GroupByValidCategory(IEnumerable<CommercialResources> resources)
    {
        var result = new Dictionary<string, Dictionary<string, List<LinkItem>>>();

        foreach (var resource in resources)
        {
            foreach (var category in resource.Category)
            {
                if (!CategoryToSubCategories.TryGetValue(category, out var validSubCategories))
                    continue;

                foreach (var subCategory in resource.SubCategory)
                {
                    if (!validSubCategories.Contains(subCategory))
                        continue;

                    if (!result.TryGetValue(category, out var subCategoryDictionary))
                    {
                        subCategoryDictionary = new Dictionary<string, List<LinkItem>>();
                        result[category] = subCategoryDictionary;
                    }

                    if (!subCategoryDictionary.TryGetValue(subCategory, out var links))
                    {
                        links = [];
                        subCategoryDictionary[subCategory] = links;
                    }

                    links.Add(new LinkItem
                    {
                        Title = resource.Title,
                        Url = resource.Url
                    });
                }
            }
        }

        return result
            .OrderBy(g => Lookups.CategoryOrderMap[g.Key])
            .Select(g => new GroupedResources
            {
                Category = g.Key,
                Sections = g.Value
                    .OrderBy(s => s.Key)
                    .Select(s => new SectionGroup
                    {
                        Section = s.Key,
                        Links = s.Value
                            .OrderBy(link => link.Title)
                            .ToList()
                    })
                    .ToList()
            }).ToList();
    }

    public static readonly Dictionary<string, string[]> CategoryToSubCategories = new()
    {
        { Category.TeachingStaff, SubCostCategories.TeachingStaff.SubCategories },
        { Category.NonEducationalSupportStaff, SubCostCategories.NonEducationalSupportStaff.SubCategories },
        { Category.EducationalSupplies, SubCostCategories.EducationalSupplies.SubCategories },
        { Category.EducationalIct, SubCostCategories.EducationalIct.SubCategories },
        { Category.PremisesStaffServices, SubCostCategories.PremisesStaffServices.SubCategories },
        { Category.Utilities, SubCostCategories.Utilities.SubCategories },
        { Category.AdministrativeSupplies, SubCostCategories.AdministrativeSupplies.SubCategories },
        { Category.CateringStaffServices, SubCostCategories.CateringStaffServices.SubCategories },
        { Category.Other, SubCostCategories.Other.SubCategories }
    };
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