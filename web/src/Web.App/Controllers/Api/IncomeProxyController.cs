using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/income")]
public class IncomeProxyController(ILogger<IncomeProxyController> logger, IIncomeApi api) : Controller
{
    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<IncomeHistoryRows>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("history")]
    [ValidateId]
    public async Task<IActionResult> History(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        CancellationToken cancellationToken = default)
    {
        using (logger.BeginScope(new
               {
                   type,
                   id
               }))
        {
            try
            {
                var query = new ApiQuery()
                    .AddIfNotNull("dimension", dimension);

                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await api.SchoolHistory(id, query, cancellationToken).GetResultOrDefault<IncomeHistoryRows>(),
                    OrganisationTypes.Trust => await api.TrustHistory(id, query, cancellationToken).GetResultOrDefault<IncomeHistoryRows>(),
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