using System.Threading.Tasks;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Benchmark.Db;

public interface ITrustDb 
{
    Task<ComparatorSet<Trust>> CreateSet(TrustComparatorSetRequest body);
}

public class TrustDb : ITrustDb 
{
    public async Task<ComparatorSet<Trust>> CreateSet(TrustComparatorSetRequest body)
    {
        var trusts = new Trust[]
        {
            new() { CompanyNo = 8253770, Name = "Bishop Konstant Catholic Academy Trust" },
            new() { CompanyNo = 13288914, Name = "Inclusive Education Trust" },
            new() { CompanyNo = 8426360, Name = "Accordia Academies Trust" },
            new() { CompanyNo = 7947806, Name = "Titan Education Trust" },
            new() { CompanyNo = 12111001, Name = "T4 Trust" },
            new() { CompanyNo = 7665550, Name = "Ebn Trust" },
            new() { CompanyNo = 10992368, Name = "Skylark Partnership" },
            new() { CompanyNo = 13057873, Name = "North Star Academy Trust" }
        };
        
       return await Task.FromResult(ComparatorSet<Trust>.Create(trusts, includeResults: body.IncludeSet));
    }
}