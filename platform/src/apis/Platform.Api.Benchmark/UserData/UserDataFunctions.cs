using System;
using System.Collections.Generic;
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

namespace Platform.Api.Benchmark.UserData;

[ApiExplorerSettings(GroupName = "User Data")]
public class UserDataFunctions
{
    private readonly ILogger<UserDataFunctions> _logger;
    private readonly IUserDataService _service;

    public UserDataFunctions(IUserDataService service, ILogger<UserDataFunctions> logger)
    {
        _service = service;
        _logger = logger;
    }

    [FunctionName(nameof(QueryAsync))]
    [ProducesResponseType(typeof(IEnumerable<UserData>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("userId", "User Id", DataType = typeof(string), Required = true)]
    [QueryStringParameter("type", "Type", DataType = typeof(string), Required = false)]
    [QueryStringParameter("organisationType", "Organisation Type", DataType = typeof(string), Required = false)]
    [QueryStringParameter("organisationId", "Organisation Id", DataType = typeof(string), Required = false)]
    [QueryStringParameter("status", "Status", DataType = typeof(string), Required = false)]
    [QueryStringParameter("id", "Identifier", DataType = typeof(string), Required = false)]
    public async Task<IActionResult> QueryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "user-data")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var userId = req.Query["userId"].ToString();
                var type = req.Query["type"].ToString();
                var status = req.Query["status"].ToString();
                var id = req.Query["id"].ToString();
                var organisationType = req.Query["organisationType"].ToString();
                var organisationId = req.Query["organisationId"].ToString();
                var data = await _service.QueryAsync(userId, type, status, id, organisationId, organisationType);

                return new JsonContentResult(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get user data");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}