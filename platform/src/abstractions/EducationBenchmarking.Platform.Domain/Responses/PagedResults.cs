using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace EducationBenchmarking.Platform.Domain.Responses;

[ExcludeFromCodeCoverage]
public record PagedResults<T> : IPagedResults
{
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T>? Results { get; set; }

    public static PagedResults<T> Create(IEnumerable<T> results, int page, int pageSize, long? totalResults = null)
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
}

public interface IPagedResults
{
    long TotalResults { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
    int PageCount { get; }
}