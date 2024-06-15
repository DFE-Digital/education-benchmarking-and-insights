using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/balance")]
public class BalanceProxyController(ILogger<BalanceProxyController> logger, IBalanceApi api) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var query = new ApiQuery().AddIfNotNull("dimension", dimension);

                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await api.SchoolHistory(id, query).GetResultOrDefault<BalanceHistory[]>(),
                    OrganisationTypes.Trust => await api.TrustHistory(id, query).GetResultOrDefault<BalanceHistory[]>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting balance history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}