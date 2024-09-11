using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenASchoolSpendingViewModel
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