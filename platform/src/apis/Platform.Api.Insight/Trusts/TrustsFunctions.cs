using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Trusts;

[ApiExplorerSettings(GroupName = "Trusts")]
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ITrustsService _service;

    public TrustsFunctions(ILogger<TrustsFunctions> logger, ITrustsService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(QueryTrustsCharacteristicsAsync))]
    [ProducesResponseType(typeof(TrustCharacteristic[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("companyNumbers", "List of trust company numbers", DataType = typeof(string[]), Required = true)]
    public async Task<IActionResult> QueryTrustsCharacteristicsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trusts/characteristics")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<TrustsParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var trusts = await _service.QueryCharacteristicAsync(queryParams.Truts);
                return new JsonContentResult(trusts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trusts characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}