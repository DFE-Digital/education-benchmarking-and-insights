using System.Threading.Tasks;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Benchmark.Db;

public interface IComparatorSetDb
{
    Task<ComparatorSet> CreateSet(ComparatorSetRequest body);
}

public class ComparatorSetDb : IComparatorSetDb
{
    public async Task<ComparatorSet> CreateSet(ComparatorSetRequest body)
    {
        var schools = new School[]
        {
            new() { Urn = "140558", Name = "St Joseph's Catholic Primary School, Moorthorpe", Kind ="Academy sponsor led", FinanceType = "Academies" },
            new() { Urn = "135558", Name = "Hawkswood Primary Pru", Kind ="Pupil referral unit", FinanceType = "Maintained" },
            new() { Urn = "105376", Name = "Cloughside College", Kind ="Community special school", FinanceType = "Maintained" },
            new() { Urn = "112858", Name = "Stoney Middleton Cofe (C) Primary School", Kind ="Voluntary controlled school", FinanceType = "Maintained" },
            new() { Urn = "122233", Name = "Kielder Primary School And Nursery", Kind ="Community school", FinanceType = "Maintained" },
            new() { Urn = "118155", Name = "Chillerton And Rookley Primary School", Kind ="Community school", FinanceType = "Maintained" },
            new() { Urn = "112267", Name = "Asby Endowed School", Kind ="Voluntary controlled school", FinanceType = "Maintained" }
        };

        return await Task.FromResult(ComparatorSet.Create(schools, includeResults: body.IncludeSet));
    }
}