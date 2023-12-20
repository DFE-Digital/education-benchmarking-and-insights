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
            new() { CompanyNumber = "8253770", Name = "Bishop Konstant Catholic Academy Trust" },
            new() { CompanyNumber = "13288914", Name = "Inclusive Education Trust" },
            new() { CompanyNumber = "8426360", Name = "Accordia Academies Trust" },
            new() { CompanyNumber = "7947806", Name = "Titan Education Trust" },
            new() { CompanyNumber = "12111001", Name = "T4 Trust" },
            new() { CompanyNumber = "7665550", Name = "Ebn Trust" },
            new() { CompanyNumber = "10992368", Name = "Skylark Partnership" },
            new() { CompanyNumber = "13057873", Name = "North Star Academy Trust" }
        };
        
       return await Task.FromResult(ComparatorSet<Trust>.Create(trusts, includeResults: body.IncludeSet));
    }
}