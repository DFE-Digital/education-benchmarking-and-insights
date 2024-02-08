using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Services;

public interface IComparatorSetService
{
    Task<ComparatorSet<School>> ReadSchoolComparatorSet(string urn);
    Task<ComparatorSet<School>> RemoveSchoolFromComparatorSet(string urn, string schoolUrnToRemove);
    Task<ComparatorSet<School>> ResetSchoolComparatorSet(string urn);
}

public class ComparatorSetService(IHttpContextAccessor httpContextAccessor, IBenchmarkApi benchmarkApi) : IComparatorSetService
{
    public async Task<ComparatorSet<School>> ReadSchoolComparatorSet(string urn)
    {
        var key = Key(urn); 
        var context = httpContextAccessor.HttpContext;
        var set = context?.Session.Get<ComparatorSet<School>>(key);
        if (set == null)
        {
            set = await benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
            context?.Session.Set(key, set);
        }
        
        return set;
    }
    
    public async Task<ComparatorSet<School>> RemoveSchoolFromComparatorSet(string urn, string schoolUrnToRemove)
    {
        var key = Key(urn); 
        var currentSet = await ReadSchoolComparatorSet(urn);
        var schools = currentSet.Results.ToList();
        schools.RemoveAll(x => x.Urn == schoolUrnToRemove);

        var newSet = new ComparatorSet<School> { TotalResults = schools.Count, Results = schools };
        var context = httpContextAccessor.HttpContext;
        context?.Session.Set(key, newSet);

        return newSet;
    }

    public async Task<ComparatorSet<School>> ResetSchoolComparatorSet(string urn)
    {
        var key = Key(urn); 
        var context = httpContextAccessor.HttpContext;
        context?.Session.Remove(key);

        return await ReadSchoolComparatorSet(urn);
    }

    private static string Key(string urn) => SessionKeys.SchoolComparatorSet(urn);
}