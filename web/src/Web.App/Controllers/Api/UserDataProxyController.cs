using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.App.Extensions;
using Web.App.Services;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/user-data")]
[Authorize]
public class UserDataProxyController(ILogger<ProxyController> logger, IUserDataService userDataService) : Controller
{
    [HttpGet]
    [Route("school/{urn}/{identifier}")]
    [Produces("application/json")]
    public async Task<IActionResult> Index(string urn, string identifier)
    {
        using (logger.BeginScope(new { identifier }))
        {
            try
            {
                var userSet = await userDataService.GetSchoolComparatorSetAsync(User.UserId(), identifier, urn);
                if (userSet == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting user data {Id} for {User}", identifier, User.UserId());
                return StatusCode(500);
            }
        }
    }
}