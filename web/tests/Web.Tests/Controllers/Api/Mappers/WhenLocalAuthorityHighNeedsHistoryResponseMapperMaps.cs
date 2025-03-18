using AutoFixture;
using Web.App.Domain.LocalAuthorities;

namespace Web.Tests.Controllers.Api.Mappers;

public abstract class WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    protected const string Code = nameof(Code);
    protected const int StartYear = 2020;
    protected const int EndYear = 2024;
    private static readonly Fixture Fixture = new();
    protected static readonly HighNeedsYear Outturn2021 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2021)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Outturn2022 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2022)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Outturn2023 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2023)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Outturn2024 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2024)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Budget2021 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2021)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Budget2022 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2022)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Budget2023 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2023)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear Budget2024 = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, 2024)
        .With(o => o.Code, Code)
        .Create();

    protected readonly HighNeedsHistory<HighNeedsYear> History = new()
    {
        StartYear = StartYear,
        EndYear = EndYear,
        Outturn =
        [
            Outturn2021,
            Outturn2022,
            Outturn2023,
            Outturn2024,
            new HighNeedsYear
            {
                Year = EndYear
            },
            new HighNeedsYear
            {
                Code = Code
            }
        ],
        Budget =
        [
            Budget2021,
            Budget2022,
            Budget2023,
            Budget2024,
            new HighNeedsYear
            {
                Year = EndYear
            },
            new HighNeedsYear
            {
                Code = Code
            }
        ]
    };

    protected readonly HighNeedsHistory<HighNeedsYear> TruncatedHistory = new()
    {
        StartYear = StartYear,
        EndYear = EndYear,
        Outturn =
        [
            Outturn2021,
            Outturn2023
        ]
    };
}