using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenASchoolSpendingViewModel
{
    private const string URN = nameof(URN);
    private readonly School _school = new Fixture()
        .Build<School>()
        .With(s => s.URN, URN)
        .Create();

    public static TheoryData<
        IEnumerable<RagRating>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<CostCategory>,
        IEnumerable<CostCategory>,
        IEnumerable<CostCategory>
    > WhenRagRatingsAreData
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
                RAG = "amber"
            };

            var educationalIctRagRating = new RagRating
            {
                Category = Category.EducationalIct,
                RAG = "green"
            };

            var otherRagRating = new RagRating
            {
                Category = Category.Other,
                RAG = "green"
            };

            return new TheoryData<
                IEnumerable<RagRating>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<CostCategory>,
                IEnumerable<CostCategory>,
                IEnumerable<CostCategory>
            >
            {
                {
                    [administrativeSuppliesRagRating, teachingStaffRagRating, educationalIctRagRating, otherRagRating], [
                        new SchoolExpenditure
                        {
                            URN = URN
                        }
                    ],
                    [
                        new SchoolExpenditure
                        {
                            URN = URN
                        }
                    ],
                    [new AdministrativeSupplies(administrativeSuppliesRagRating)], [new TeachingStaff(teachingStaffRagRating)], [new EducationalIct(educationalIctRagRating)]
                }
            };
        }
    }

    [Theory]
    [MemberData(nameof(WhenRagRatingsAreData))]
    public void WhenRagRatingsAre(
        IEnumerable<RagRating> ratings,
        IEnumerable<SchoolExpenditure> pupilExpenditure,
        IEnumerable<SchoolExpenditure> areaExpenditure,
        IEnumerable<CostCategory> expectedHighPriorityCosts,
        IEnumerable<CostCategory> expectedMediumPriorityCosts,
        IEnumerable<CostCategory> expectedLowPriorityCosts)
    {
        var actual = new SchoolSpendingViewModel(_school, ratings, pupilExpenditure, areaExpenditure);
        Assert.Equal(expectedHighPriorityCosts.Select(c => c.Rating), actual.HighPriorityCosts.Select(c => c.Rating));
        Assert.Equal(expectedMediumPriorityCosts.Select(c => c.Rating), actual.MediumPriorityCosts.Select(c => c.Rating));
        Assert.Equal(expectedLowPriorityCosts.Select(c => c.Rating), actual.LowPriorityCosts.Select(c => c.Rating));
    }

    public class Stats
    {
        public static TheoryData<RagRating, ChartStatsViewModel> WhenRagRatingIsData =>
            new()
            {
                {
                    new RagRating
                    {
                        Mean = 1000,
                        DiffMean = 500
                    },
                    new ChartStatsViewModel
                    {
                        Average = 1000,
                        Difference = 500,
                        PercentDifference = 50
                    }
                },
                {
                    new RagRating(), new ChartStatsViewModel()
                },
                {
                    new RagRating
                    {
                        Mean = 0,
                        DiffMean = 0
                    },
                    new ChartStatsViewModel
                    {
                        Average = 0,
                        Difference = 0,
                        PercentDifference = 0
                    }
                }
            };

        [Theory]
        [MemberData(nameof(WhenRagRatingIsData))]
        public void WhenRagRatingIs(RagRating ragRating, ChartStatsViewModel expected)
        {
            var actual = SchoolSpendingViewModel.Stats(ragRating);
            Assert.Equivalent(expected, actual);
        }
    }
}