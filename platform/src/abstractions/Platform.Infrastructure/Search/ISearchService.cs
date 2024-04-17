namespace Platform.Infrastructure.Search;

public interface ISearchService<T>
{
    Task<SearchResponseModel<T>> SearchAsync(PostSearchRequestModel request);
    Task<SuggestResponseModel<T>> SuggestAsync(PostSuggestRequestModel request);
}