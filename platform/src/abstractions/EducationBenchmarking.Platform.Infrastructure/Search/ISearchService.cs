namespace EducationBenchmarking.Platform.Infrastructure.Search;

public interface ISearchService<T>
{
    Task<SearchOutput<T>> SearchAsync(PostSearchRequest request);
}