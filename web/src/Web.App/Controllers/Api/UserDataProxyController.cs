using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/user-data")]
[Authorize]
public class UserDataProxyController(ILogger<ProxyController> logger, IUserDataApi userDataApi) : Controller
{
    [HttpGet]
    [Route("{identifier}")]
    [Produces("application/json")]
    public async Task<IActionResult> Index(string identifier)
    {
        using (logger.BeginScope(new { identifier }))
        {
            try
            {
                var query = new ApiQuery()
                    .AddIfNotNull("userId", User.UserId())
                    .AddIfNotNull("id", identifier);

                var userSets = await userDataApi.GetAsync(query).GetResultOrDefault<UserData[]>();
                if (userSets == null || userSets.Length == 0)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userSets.FirstOrDefault());
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting user data {Id} for {User}", identifier, User.UserId());
                return StatusCode(500);
            }
        }
    }
}