using System.Diagnostics.CodeAnalysis;
using Web.App.Domain.Content;

namespace Web.App.ViewModels;

[ExcludeFromCodeCoverage]
public class NewsArticleViewModel(News? news)
{
    public string? Body => news?.Body;
    public string? Title => news?.Title;
}