using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
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
    [Route("school/{urn}")]
    [ValidateUrn]
    [Produces("application/json")]
    [ProducesResponseType<UserData>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SchoolUserData(string urn, CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userSet = await userDataService.GetSchoolComparatorSetActiveAsync(User, urn, cancellationToken);
                if (userSet == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting school user data for {User}", User.UserGuid());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("trust/{companyNumber}")]
    [ValidateCompanyNumber]
    [Produces("application/json")]
    public async Task<IActionResult> TrustUserData(string companyNumber, CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                var userSet = await userDataService.GetTrustComparatorSetAsync(User, companyNumber, cancellationToken);
                if (userSet == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting trust user data for {User}", User.UserGuid());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("school/custom-data/{urn}")]
    [ValidateUrn]
    [Produces("application/json")]
    [ProducesResponseType<UserData>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SchoolCustomDataUserData(string urn, CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userData = await userDataService.GetCustomDataActiveAsync(User, urn, cancellationToken);
                if (userData == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(userData);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting school custom data user data for {User}", User.UserGuid());
                return StatusCode(500);
            }
        }
    }
}