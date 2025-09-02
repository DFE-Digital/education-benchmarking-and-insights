using System.Diagnostics.CodeAnalysis;
using Web.App.Domain.Content;

namespace Web.App.ViewModels;

[ExcludeFromCodeCoverage]
public class NewsViewModel(News[] news)
{
    public NewsItemViewModel[] Items => news.Select(n => new NewsItemViewModel(n)).ToArray();
}

[ExcludeFromCodeCoverage]
public class NewsItemViewModel(News news)
{
    public string? Title => news.Title;
    public string? Slug => news.Slug;
    public DateTimeOffset? Published => news.Published;
}