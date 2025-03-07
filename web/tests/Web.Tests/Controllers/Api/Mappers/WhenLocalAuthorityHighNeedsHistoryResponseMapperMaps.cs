using AutoFixture;
using Web.App.Domain.LocalAuthorities;

namespace Web.Tests.Controllers.Api.Mappers;

public abstract class WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    private static readonly Fixture Fixture = new();
    protected const string Code = nameof(Code);
    protected const int StartYear = 2021;
    protected const int EndYear = 2024;
    protected static readonly HighNeedsYear OutturnStartYear = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, StartYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear OutturnEndYear = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, EndYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear BudgetStartYear = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, StartYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly HighNeedsYear BudgetEndYear = Fixture
        .Build<HighNeedsYear>()
        .With(o => o.Year, EndYear)
        .With(o => o.Code, Code)
        .Create();

    protected readonly HighNeedsHistory<HighNeedsYear> History = new HighNeedsHistory<HighNeedsYear>
    {
        StartYear = StartYear,
        EndYear = EndYear,
        Outturn =
        [
            OutturnStartYear,
            OutturnEndYear,
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
            BudgetStartYear,
            BudgetEndYear,
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
}