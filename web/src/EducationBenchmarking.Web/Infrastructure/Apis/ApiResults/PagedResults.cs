namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PagedResults<T> : IEquatable<PagedResults<T>>
{
    public int TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T>? Results { get; set; }
    
    public IEnumerable<int> GetPageWindow(int windowSize = 5)
    {
        if (Page < windowSize)
        {
            foreach (var i in Enumerable.Range(1, Math.Min(PageCount, windowSize)))
            {
                yield return i;
            }
        }
        else
        {
            var delta = (int)Math.Floor(windowSize / 2.0);
            for (int i = Math.Max(1, Page - delta); i <= Math.Min(PageCount, Page + delta); i++)
            {
                yield return i;
            }
        }
    }
    
    public bool Equals(PagedResults<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return TotalResults == other.TotalResults
               && Page == other.Page
               && PageSize == other.PageSize
               && (Results?.SequenceEqual(other.Results!) ?? false);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PagedResults<T>)obj);
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