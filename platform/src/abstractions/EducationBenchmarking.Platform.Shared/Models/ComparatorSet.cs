namespace EducationBenchmarking.Platform.Shared;

public class ComparatorSet<T>
{
    public int TotalResults { get; set; }
    public IEnumerable<T>? Results { get; set; }
        
    public static ComparatorSet<T> Create(IEnumerable<T> results, int? totalResults = null, bool includeResults = false)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;
            
        return new ComparatorSet<T>
        {
            Results = includeResults ? enumerable : null,
            TotalResults = resultCount
        };            
    }
}