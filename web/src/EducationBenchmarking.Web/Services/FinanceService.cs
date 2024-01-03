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

public class FinanceService : IFinanceService
{
    private readonly IInsightApi _insightApi;
    private readonly IBenchmarkApi _benchmarkApi;

    public FinanceService(IInsightApi insightApi, IBenchmarkApi benchmarkApi)
    {
        _insightApi = insightApi;
        _benchmarkApi = benchmarkApi;
    }

    public async Task<FinanceYears> GetYears()
    {
        return await _insightApi.GetFinanceYears().GetResultOrThrow<FinanceYears>();
    }

    public async Task<PagedResults<SchoolExpenditure>> GetExpenditure(string urn)
    {
        var set = await _benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
        var query = BuildApiQueryFromComparatorSet(set);
        return await _insightApi.GetSchoolsExpenditure(query).GetPagedResultOrThrow<SchoolExpenditure>();
    }

    public async Task<PagedResults<SchoolWorkforce>> GetWorkforce(string urn)
    {
        var set = await _benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
        var query = BuildApiQueryFromComparatorSet(set);
        return await _insightApi.GetSchoolsWorkforce(query).GetPagedResultOrThrow<SchoolWorkforce>();
    }

    public async Task<(Finances, Rating[])> GetRatings(School school)
    {
        var finances = await GetFinances(school);
        var fsmBanding = await _benchmarkApi.GetFreeSchoolMealBandings().GetResultOrThrow<Banding[]>();

        var sizeQuery = new ApiQuery()
            .AddIfNotNull("phase", finances.OverallPhase)
            .AddIfNotNull("hasSixthForm", finances.HasSixthForm.ToString())
            .AddIfNotNull("noOfPupils", finances.NumberOfPupils.ToString(CultureInfo.InvariantCulture))
            .AddIfNotNull("term", $"{finances.YearEnd - 1}/{finances.YearEnd}");
        
        var sizeBandings = await _benchmarkApi.GetSchoolSizeBandings(sizeQuery).GetResultOrThrow<Banding[]>();
        
        var ratingsQuery = new ApiQuery()
            .AddIfNotNull("phase", finances.OverallPhase)
            .AddIfNotNull("size", sizeBandings[0].Scale)
            .AddIfNotNull("term", $"{finances.YearEnd - 1}/{finances.YearEnd}");
        var ratings = await _insightApi.GetSchoolsRatings(ratingsQuery).GetResultOrThrow<Rating[]>();
        
        return (finances, ratings);
    }

    public async Task<Finances> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await _insightApi.GetAcademyFinances(school.Urn).GetResultOrThrow<Finances>();
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await _insightApi.GetMaintainedSchoolFinances(school.Urn).GetResultOrThrow<Finances>();
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