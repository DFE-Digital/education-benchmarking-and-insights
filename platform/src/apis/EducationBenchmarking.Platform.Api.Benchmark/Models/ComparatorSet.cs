using System.Collections.Generic;
using System.Linq;
using EducationBenchmarking.Platform.Shared;

namespace EducationBenchmarking.Platform.Api.Benchmark.Models;

public class ComparatorSet
{
    public int TotalResults { get; set; }
    public IEnumerable<School> Results { get; set; }
        
    public static ComparatorSet Create(IEnumerable<School> results, int? totalResults = null, bool includeResults = false)
    {
        var enumerable = results as School[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;
            
        return new ComparatorSet
        {
            Results = includeResults ? enumerable : null,
            TotalResults = resultCount
        };            
    }
}