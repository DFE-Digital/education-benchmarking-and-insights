using AutoFixture;
using Web.App.Domain.LocalAuthorities;

namespace Web.Tests.Controllers.Api.Mappers;

public abstract class WhenLocalAuthorityHighNeedsHistoryResponseMapperMaps
{
    private static readonly Fixture Fixture = new();
    protected const string Code = nameof(Code);
    protected const int StartYear = 2021;
    protected const int EndYear = 2024;
    protected static readonly LocalAuthorityHighNeedsYear OutturnStartYear = Fixture
        .Build<LocalAuthorityHighNeedsYear>()
        .With(o => o.Year, StartYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly LocalAuthorityHighNeedsYear OutturnEndYear = Fixture
        .Build<LocalAuthorityHighNeedsYear>()
        .With(o => o.Year, EndYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly LocalAuthorityHighNeedsYear BudgetStartYear = Fixture
        .Build<LocalAuthorityHighNeedsYear>()
        .With(o => o.Year, StartYear)
        .With(o => o.Code, Code)
        .Create();
    protected static readonly LocalAuthorityHighNeedsYear BudgetEndYear = Fixture
        .Build<LocalAuthorityHighNeedsYear>()
        .With(o => o.Year, EndYear)
        .With(o => o.Code, Code)
        .Create();

    protected readonly HighNeedsHistory<LocalAuthorityHighNeedsYear> History = new HighNeedsHistory<LocalAuthorityHighNeedsYear>
    {
        StartYear = StartYear,
        EndYear = EndYear,
        Outturn =
        [
            OutturnStartYear,
            OutturnEndYear,
            new LocalAuthorityHighNeedsYear
            {
                Year = EndYear
            },
            new LocalAuthorityHighNeedsYear
            {
                Code = Code
            }
        ],
        Budget =
        [
            BudgetStartYear,
            BudgetEndYear,
            new LocalAuthorityHighNeedsYear
            {
                Year = EndYear
            },
            new LocalAuthorityHighNeedsYear
            {
                Code = Code
            }
        ]
    };
}