using Microsoft.Extensions.Primitives;

namespace EducationBenchmarking.Platform.Shared.Helpers;

public static class QueryParameters
{
    public static (int Page,int PageSize) GetPagingValues(IEnumerable<KeyValuePair<string, StringValues>> query)
    {
        var dict = query.ToDictionary(r => r.Key, r => r.Value, StringComparer.OrdinalIgnoreCase);

        dict.TryGetValue("page", out var pageValue);
        dict.TryGetValue("pageSize", out var pageSizeValue);
        
        var pageParsed = int.TryParse(pageValue.ToString(), out var page); 
        var pageSizedParsed = int.TryParse(pageSizeValue.ToString(), out var pageSize);
        
        return (pageParsed ? page : 1, pageSizedParsed ? pageSize : 10);
    }
}