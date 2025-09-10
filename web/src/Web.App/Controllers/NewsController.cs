using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain.Content;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.News)]
[Route("news")]
public class NewsController(ILogger<NewsController> logger, INewsApi newsApi) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
    {
        try
        {
            var news = await newsApi
                .GetNews(cancellationToken)
                .GetResultOrDefault<News[]>() ?? [];

            return View(new NewsViewModel(news));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error displaying news articles: {DisplayUrl}", Request.GetDisplayUrl());
            return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
        }
    }

    [HttpGet]
    [Route("{slug}")]
    public async Task<IActionResult> Article(string slug, CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
        {
            slug
        }))
        {
            try
            {
                var news = await newsApi
                    .GetNewsArticle(slug, cancellationToken)
                    .GetResultOrDefault<News>();
                if (news == null)
                {
                    return NotFound();
                }

                return View(new NewsArticleViewModel(news));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying news article: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}