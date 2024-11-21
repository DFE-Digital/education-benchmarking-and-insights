using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenASchoolResourcesViewModel
{
    private const string URN = nameof(URN);
    private readonly School _school = new Fixture()
        .Build<School>()
        .With(s => s.URN, URN)
        .Create();

    public static TheoryData<IEnumerable<RagRating>, IEnumerable<CostCategory>> WhenRagRatingsAreData
    {
        get
        {
            var administrativeSuppliesRagRating = new RagRating
            {
                Category = Category.AdministrativeSupplies,
                RAG = "red"
            };

            var teachingStaffRagRating = new RagRating
            {
                Category = Category.TeachingStaff,
                RAG = "amber",
                Median = 0,
                Value = 1
            };

            var educationalIctRagRating = new RagRating
            {
                Category = Category.EducationalIct,
                RAG = "green"
            };

            var educationalSuppliesRagRating = new RagRating
            {
                Category = Category.EducationalSupplies,
                RAG = "red",
                Median = 1,
                Value = 0
            };

            var otherRagRating = new RagRating
            {
                Category = Category.Other,
                RAG = "red"
            };

            return new TheoryData<IEnumerable<RagRating>, IEnumerable<CostCategory>>
            {
                {
                    [administrativeSuppliesRagRating, teachingStaffRagRating, educationalIctRagRating, educationalSuppliesRagRating, otherRagRating], [new AdministrativeSupplies(administrativeSuppliesRagRating), new TeachingStaff(teachingStaffRagRating)]
                }
            };
        }
    }

    [Theory]
    [MemberData(nameof(WhenRagRatingsAreData))]
    public void WhenRagRatingsAre(
        IEnumerable<RagRating> ratings, IEnumerable<CostCategory> expectedCostCategories)
    {
        var actual = new SchoolResourcesViewModel(_school, ratings);
        Assert.Equal(expectedCostCategories.Select(c => c.Rating), actual.CostCategories.Select(c => c.Rating));
    }
}