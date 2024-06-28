using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Services;
namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/user-data")]
[Authorize]
public class UserDataProxyController(ILogger<UserDataProxyController> logger, IUserDataService userDataService) : Controller
{
    [HttpGet]
    [Route("school/{urn}/{identifier}")]
    [Produces("application/json")]
    [ProducesResponseType<UserData>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SchoolUserData(string urn, string identifier)
    {
        using (logger.BeginScope(new
        {
            identifier
        }))
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
                logger.LogError(e, "An error getting school user data {Id} for {User}", identifier, User.UserId());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("trust/{companyNumber}/{identifier}")]
    [Produces("application/json")]
    public async Task<IActionResult> TrustUserData(string companyNumber, string identifier)
    {
        using (logger.BeginScope(new
        {
            identifier
        }))
        {
            try
            {
                var userSet = await userDataService.GetTrustComparatorSetAsync(User.UserId(), identifier, companyNumber);
                if (userSet == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting trust user data {Id} for {User}", identifier, User.UserId());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("school/custom-data/{urn}/{identifier}")]
    [Produces("application/json")]
    [ProducesResponseType<UserData>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SchoolCustomDataUserData(string urn, string identifier)
    {
        using (logger.BeginScope(new
        {
            identifier
        }))
        {
            try
            {
                var userData = await userDataService.GetCustomDataAsync(User.UserId(), identifier, urn);
                if (userData == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userData);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting school custom data user data {Id} for {User}", identifier, User.UserId());
                return StatusCode(500);
            }
        }
    }
}