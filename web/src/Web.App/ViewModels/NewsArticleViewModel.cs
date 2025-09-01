using Web.App.Domain.Content;

namespace Web.App.ViewModels;

public class NewsArticleViewModel(News? news)
{
    public string? Body => news?.Body;
    public string? Title => news?.Title;
}