using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Tests.Domain;

public class GivenACommercialResourcesViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void BuilderShouldOrderAndGroupCorrectly()
    {
        // create resources with at least one of each cost category 
        // then many with reoccurring section values randomly to test ordering and grouping
        var commonSections = new[] { "Test", "Test1", "Test2", "Test3", "Test4" };
        var guaranteedResources = Category.All
            .SelectMany(category => commonSections
                .Select(section => _fixture.Build<CommercialResources>()
                    .With(r => r.Category, category)
                    .With(r => r.SubCategory, section)
                    .Create()))
            .ToList();

        var randomResources = _fixture.Build<CommercialResources>()
            .With(r => r.Category, () => Category.All[Random.Shared.Next(Category.All.Length)])
            .With(r => r.SubCategory, () => commonSections[Random.Shared.Next(commonSections.Length)])
            .CreateMany(90)
            .ToList();

        var resources = guaranteedResources.Concat(randomResources).ToArray();

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
                    .Where(r => r.Category == group.Category && r.SubCategory == section.Section).ToList();
                Assert.Equal(expectedSections.Count, section.Links.Count);

                foreach (var link in section.Links)
                {
                    var matchingResource =
                        expectedSections.FirstOrDefault(r => r.Title == link.Title && r.Url == link.Url);
                    Assert.NotNull(matchingResource);
                }
            }
        }
    }
}