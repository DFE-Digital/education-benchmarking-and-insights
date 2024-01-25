using System.Globalization;
using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Services;

public interface IFinanceService
{
    Task<PagedResults<SchoolExpenditure>> GetExpenditure(string urn);
    Task<PagedResults<SchoolWorkforce>> GetWorkforce(string urn);
    Task<(Finances, Rating[])> GetRatings(School school);
    Task<Finances> GetFinances(School school);
    Task<Finances> GetFinances(Trust trust);
    Task<FinanceYears> GetYears();
}

public class FinanceService(IInsightApi insightApi, IBenchmarkApi benchmarkApi) : IFinanceService
{
    public async Task<FinanceYears> GetYears()
    {
        return await insightApi.GetFinanceYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<PagedResults<SchoolExpenditure>> GetExpenditure(string urn)
    {
        var set = await benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
        var query = BuildApiQueryFromComparatorSet(set);
        return await insightApi.GetSchoolsExpenditure(query).GetPagedResultOrThrow<SchoolExpenditure>();
    }

    public async Task<PagedResults<SchoolWorkforce>> GetWorkforce(string urn)
    {
        var set = await benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
        var query = BuildApiQueryFromComparatorSet(set);
        return await insightApi.GetSchoolsWorkforce(query).GetPagedResultOrThrow<SchoolWorkforce>();
    }

    public async Task<(Finances, Rating[])> GetRatings(School school)
    {
        var finances = await GetFinances(school);
        var fsmBandings = await benchmarkApi.GetFreeSchoolMealBandings().GetResultOrThrow<Banding[]>();
        
        var sizeQuery = new ApiQuery()
            .AddIfNotNull("phase", finances.OverallPhase)
            .AddIfNotNull("hasSixthForm", finances.HasSixthForm.ToString())
            .AddIfNotNull("noOfPupils", finances.NumberOfPupils.ToString(CultureInfo.InvariantCulture))
            .AddIfNotNull("term", $"{finances.YearEnd - 1}/{finances.YearEnd}");
        var sizeBandings = await benchmarkApi.GetSchoolSizeBandings(sizeQuery).GetResultOrThrow<Banding[]>();
        
        var ratingsQuery = new ApiQuery()
            .AddIfNotNull("phase", finances.OverallPhase)
            .AddIfNotNull("size", fsmBandings.FirstOrDefault()?.Scale)
            .AddIfNotNull("fsm", sizeBandings.FirstOrDefault()?.Scale)
            .AddIfNotNull("term", $"{finances.YearEnd - 1}/{finances.YearEnd}");
        var ratings = await insightApi.GetSchoolsRatings(ratingsQuery).GetResultOrThrow<Rating[]>();
        
        return (finances, ratings);
    }

    public async Task<Finances> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await insightApi.GetAcademyFinances(school.Urn).GetResultOrThrow<Finances>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await insightApi.GetMaintainedSchoolFinances(school.Urn).GetResultOrThrow<Finances>();
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }
    
    public async Task<Finances> GetFinances(Trust trust)
    {
        return new Finances();
    }
    
    private static ApiQuery BuildApiQueryFromComparatorSet(ComparatorSet<School> set)
    {
        var query = new ApiQuery().Page(1, set.TotalResults);
        foreach (var school in set.Results)
        {
            query.AddIfNotNull("urns", school.Urn);
        }

        return query;
    }
}