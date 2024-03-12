namespace Platform.Infrastructure.Search;

public interface ISearchService<T>
{
    Task<SearchOutput<T>> SearchAsync(PostSearchRequest request);
    Task<SuggestOutput<T>> SuggestAsync(PostSuggestRequest request);
}