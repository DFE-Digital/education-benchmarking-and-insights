using Microsoft.AspNetCore.Mvc;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/429")]
public class Test429ProxyController(IInsightApi insightApi) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Index()
    {
        await insightApi.Test429().EnsureSuccess();
        return new JsonResult(new
        {
            message = "OK"
        });
    }
}