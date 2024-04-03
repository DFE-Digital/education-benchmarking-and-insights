using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record PagedResponseModel<T> : IPagedResponse
{
    public long TotalResults { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
    public IEnumerable<T>? Results { get; set; }

    public static PagedResponseModel<T> Create(IEnumerable<T> results, int page, int pageSize, long? totalResults = null)
    {
        var enumerable = results as T[] ?? results.ToArray();
        var resultCount = totalResults ?? enumerable.Length;

        return new PagedResponseModel<T>
        {
            Page = page,
            PageSize = pageSize,
            Results = enumerable,
            TotalResults = resultCount
        };
    }
}

public interface IPagedResponse
{
    long TotalResults { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
    int PageCount { get; }
}