using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/expenditure")]
public class ExpenditureProxyController(ILogger<ExpenditureProxyController> logger, IExpenditureApi api) : Controller
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
                    OrganisationTypes.School => await api.SchoolHistory(id, query).GetResultOrDefault<ExpenditureHistory[]>(),
                    OrganisationTypes.Trust => await api.TrustHistory(id, query).GetResultOrDefault<ExpenditureHistory[]>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting expenditure history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}