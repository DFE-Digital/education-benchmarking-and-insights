using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenACommercialResourcesViewModel
{
    [Fact]
    public void BuilderShouldOrderAndGroupCorrectly()
    {
        var resources = new List<CommercialResources>();

        foreach (var category in Category.All)
        {
            if (!CommercialResourcesBuilder.CategoryToSubCategories.TryGetValue(category, out var validSubCategories))
                continue;

            foreach (var subCategory in validSubCategories)
            {
                resources.Add(new CommercialResources
                {
                    Title = $"Resource for {category} - {subCategory}",
                    Url = $"https://example.com/{category}/{subCategory}",
                    Category = [category],
                    SubCategory = [subCategory]
                });
            }
        }

        var actual = CommercialResourcesBuilder.GroupByValidCategory(resources);

        var groupedResources = actual.ToList();

        var actualOrder = groupedResources.Select(g => g.Category!).ToList();
        Assert.Equal(Category.All, actualOrder);

        foreach (var group in groupedResources)
        {
            Assert.NotNull(group.Category);
            Assert.Contains(group.Category, Category.All);
            Assert.NotEmpty(group.Sections);

            foreach (var section in group.Sections)
            {
                Assert.NotNull(section.Section);
                Assert.NotEmpty(section.Links);

                var expectedSections = resources
                    .Where(r => r.Category.Contains(group.Category) && r.SubCategory.Contains(section.Section))
                    .ToList();

                Assert.Equal(expectedSections.Count, section.Links.Count);

                foreach (var link in section.Links)
                {
                    var matchingResource = expectedSections
                        .FirstOrDefault(r => r.Title == link.Title && r.Url == link.Url);
                    Assert.NotNull(matchingResource);
                }
            }
        }
    }
}