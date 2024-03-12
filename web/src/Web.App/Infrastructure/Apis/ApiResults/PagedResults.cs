using System.Diagnostics.CodeAnalysis;

namespace Web.App.Infrastructure.Apis
{
    [ExcludeFromCodeCoverage]
    public record PagedResults<T>
    {
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount => (int)Math.Ceiling(TotalResults / (float)Math.Max(1, PageSize));
        public IEnumerable<T>? Results { get; set; }
    }
}