namespace EducationBenchmarking.Platform.Shared;


public class PagedResults<T> : IPagedResults, IEquatable<PagedResults<T>>
{
    public int TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T> Results { get; set; }
        
    public static PagedResults<T> Create(IEnumerable<T> results, int page = 1, int pageSize = 10,  int?  totalResults = null)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;
            
        return new PagedResults<T>
        {
            Page = page,
            PageSize = pageSize,
            Results = enumerable,
            TotalResults = resultCount
        };            
    }
    
    public bool Equals(PagedResults<T> other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TotalResults == other.TotalResults 
               && Page == other.Page 
               && PageSize == other.PageSize 
               && (Results?.SequenceEqual(other.Results) ?? false);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PagedResults<T>) obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = TotalResults;
            hashCode = (hashCode * 397) ^ Page;
            hashCode = (hashCode * 397) ^ PageSize;
            hashCode = (hashCode * 397) ^ (Results != null ? Results.GetHashCode() : 0);
            return hashCode;
        }
    }
}

public interface IPagedResults
{
    int TotalResults { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
    int PageCount { get; }
}