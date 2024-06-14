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

namespace Platform.Api.Insight.Balance;

[ApiExplorerSettings(GroupName = "Balance")]
public class BalanceFunctions
{

    private readonly ILogger<BalanceFunctions> _logger;
    private readonly IBalanceService _service;

    public BalanceFunctions(ILogger<BalanceFunctions> logger, IBalanceService service)
    {
        _logger = logger;
        _service = service;
    }


    [FunctionName(nameof(BalanceAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult BalanceAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(BalanceDimensions.All);
        }
    }

    [FunctionName(nameof(SchoolBalanceAsync))]
    [ProducesResponseType(typeof(SchoolBalanceResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}")]
        HttpRequest req,
        string urn)
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
                var dimension = req.Query["dimension"].ToString();

                if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = BalanceDimensions.Actuals;
                }

                var result = await _service.GetSchoolAsync(urn);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(BalanceResponseFactory.Create(result, dimension));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school balance");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(TrustBalanceAsync))]
    [ProducesResponseType(typeof(TrustBalanceResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> TrustBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}")]
        HttpRequest req,
        string companyNumber)
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
                var dimension = req.Query["dimension"].ToString();
                if (!BalanceDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = BalanceDimensions.Actuals;
                }

                var result = await _service.GetTrustAsync(companyNumber);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(BalanceResponseFactory.Create(result, dimension));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get balance");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolBalanceHistoryAsync))]
    [ProducesResponseType(typeof(SchoolBalanceHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> SchoolBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}/history")]
        HttpRequest req,
        string urn)
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
                //TODO: Add validation for dimension
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.GetSchoolHistoryAsync(urn);
                return new JsonContentResult(result.Select(x => BalanceResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustBalanceHistoryAsync))]
    [ProducesResponseType(typeof(SchoolBalanceHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> TrustBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}/history")]
        HttpRequest req,
        string companyNumber)
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
                //TODO: Add validation for dimension
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.GetTrustHistoryAsync(companyNumber);
                return new JsonContentResult(result.Select(x => BalanceResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust balance history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsBalanceAsync))]
    [ProducesResponseType(typeof(SchoolBalanceResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QuerySchoolsBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/schools")]
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
                //TODO: Add validation for urns and dimension
                var urns = req.Query["urns"].ToString().Split(",");
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.QuerySchoolsAsync(urns);
                return new JsonContentResult(result.Select(x => BalanceResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query schools balance");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryTrustsBalanceAsync))]
    [ProducesResponseType(typeof(TrustBalanceResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("companyNumbers", "List of trust company numberss", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QueryTrustsBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trusts")]
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
                //TODO: Add validation for companyNumbers and dimension
                var companyNumbers = req.Query["companyNumbers"].ToString().Split(",");
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.QueryTrustsAsync(companyNumbers);
                return new JsonContentResult(result.Select(x => BalanceResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query trusts balance");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}