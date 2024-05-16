using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/income")]
public class IncomeProxyController(ILogger<IncomeProxyController> logger, IIncomeApi incomeApi) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [Route("history")]
    public async Task<IActionResult> EstablishmentIncomeHistory([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var query = new ApiQuery().AddIfNotNull("dimension", dimension);

                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await incomeApi.SchoolHistory(id, query).GetResultOrDefault<Income[]>(),
                    OrganisationTypes.Trust => await incomeApi.TrustHistory(id, query).GetResultOrDefault<Income[]>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting income history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}