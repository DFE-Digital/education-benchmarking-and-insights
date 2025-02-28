using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewModels.Components;

public class GivenALocalAuthorityHighNeedsNationalRankingsViewModel
{
    private static readonly Fixture Fixture = new();
    private static readonly LocalAuthorityRank Rank1 = BuildRank(1);
    private static readonly LocalAuthorityRank Rank2 = BuildRank(2);
    private static readonly LocalAuthorityRank Rank3 = BuildRank(3);
    private static readonly LocalAuthorityRank Rank4 = BuildRank(4);
    private static readonly LocalAuthorityRank Rank5 = BuildRank(5);
    private static readonly LocalAuthorityRank Rank6 = BuildRank(6);
    private static readonly LocalAuthorityRank Rank7 = BuildRank(7);
    private static readonly LocalAuthorityRank Rank8 = BuildRank(8);
    private static readonly LocalAuthorityRank Rank9 = BuildRank(9);
    private static readonly LocalAuthorityRanking Ranking = new()
    {
        Ranking =
        [
            Rank1,
            Rank2,
            Rank3,
            Rank4,
            Rank5,
            Rank6,
            Rank7,
            Rank8,
            Rank9
        ]
    };

    public static TheoryData<
        string,
        int,
        LocalAuthorityRank[]
    > WhenClosestAreData => new()
    {
        {
            Rank1.Code!,
            5,
            [Rank1, Rank2, Rank3, Rank4, Rank5]
        },
        {
            Rank2.Code!,
            5,
            [Rank1, Rank2, Rank3, Rank4, Rank5]
        },
        {
            Rank3.Code!,
            5,
            [Rank1, Rank2, Rank3, Rank4, Rank5]
        },
        {
            Rank4.Code!,
            5,
            [Rank2, Rank3, Rank4, Rank5, Rank6]
        },
        {
            Rank5.Code!,
            5,
            [Rank3, Rank4, Rank5, Rank6, Rank7]
        },
        {
            Rank6.Code!,
            5,
            [Rank4, Rank5, Rank6, Rank7, Rank8]
        },
        {
            Rank7.Code!,
            5,
            [Rank5, Rank6, Rank7, Rank8, Rank9]
        },
        {
            Rank8.Code!,
            5,
            [Rank5, Rank6, Rank7, Rank8, Rank9]
        },
        {
            Rank9.Code!,
            5,
            [Rank5, Rank6, Rank7, Rank8, Rank9]
        },
        {
            Rank1.Code!,
            3,
            [Rank1, Rank2, Rank3]
        },
        {
            Rank4.Code!,
            4,
            [Rank2, Rank3, Rank4, Rank5]
        },
        {
            Rank1.Code!,
            10,
            [Rank1, Rank2, Rank3, Rank4, Rank5, Rank6, Rank7, Rank8, Rank9]
        }
    };

    [Theory]
    [MemberData(nameof(WhenClosestAreData))]
    public void ShouldReturnClosestRanksInGivenWindow(string code, int count, LocalAuthorityRank[] expected)
    {
        var vm = new LocalAuthorityHighNeedsNationalRankingsViewModel(code, Ranking, count);
        Assert.Equal(expected.Select(e => e.Rank), vm.Closest.Select(c => c.Rank));
    }

    [Fact]
    public void ShouldReturnEmptyClosestRanksForEmptyInput()
    {
        var vm = new LocalAuthorityHighNeedsNationalRankingsViewModel("code", new LocalAuthorityRanking(), 5);
        Assert.Equal([], vm.Closest);
    }

    [Fact]
    public void ShouldReturnEmptyClosestRanksForNullInput()
    {
        var vm = new LocalAuthorityHighNeedsNationalRankingsViewModel("code", null, 5);
        Assert.Equal([], vm.Closest);
    }

    private static LocalAuthorityRank BuildRank(int rank)
    {
        return Fixture
            .Build<LocalAuthorityRank>()
            .With(r => r.Rank, rank)
            .Create();
    }
}