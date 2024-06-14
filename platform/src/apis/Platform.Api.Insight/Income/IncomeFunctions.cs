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

namespace Platform.Api.Insight.Income;

[ApiExplorerSettings(GroupName = "Income")]
public class IncomeFunctions
{

    private readonly ILogger<IncomeFunctions> _logger;
    private readonly IIncomeService _service;

    public IncomeFunctions(ILogger<IncomeFunctions> logger, IIncomeService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(IncomeAllCategories))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult IncomeAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/categories")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(IncomeCategories.All);
        }
    }


    [FunctionName(nameof(IncomeAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult IncomeAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(IncomeDimensions.All);
        }
    }

    [FunctionName(nameof(SchoolIncomeAsync))]
    [ProducesResponseType(typeof(SchoolIncomeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Income category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> SchoolIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}")]
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
                var category = req.Query["category"].ToString();
                if (!IncomeCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
                {
                    category = null;
                }

                var dimension = req.Query["dimension"].ToString();
                if (!IncomeDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = IncomeDimensions.Actuals;
                }

                var result = await _service.GetSchoolAsync(urn);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(IncomeResponseFactory.Create(result, dimension, category));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school income");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(TrustIncomeAsync))]
    [ProducesResponseType(typeof(TrustIncomeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Income category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    public async Task<IActionResult> TrustIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}")]
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
                var category = req.Query["category"].ToString();
                if (!IncomeCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
                {
                    category = null;
                }

                var dimension = req.Query["dimension"].ToString();
                if (!IncomeDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
                {
                    dimension = IncomeDimensions.Actuals;
                }

                var result = await _service.GetTrustAsync(companyNumber);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(IncomeResponseFactory.Create(result, dimension, category));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get income");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolIncomeHistoryAsync))]
    [ProducesResponseType(typeof(SchoolIncomeHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> SchoolIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}/history")]
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
                return new JsonContentResult(result.Select(x => IncomeResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustIncomeHistoryAsync))]
    [ProducesResponseType(typeof(SchoolIncomeHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> TrustIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}/history")]
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
                return new JsonContentResult(result.Select(x => IncomeResponseFactory.Create(x, dimension)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust income history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsIncomeAsync))]
    [ProducesResponseType(typeof(SchoolIncomeResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Income category", DataType = typeof(string), Required = true)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QuerySchoolsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/schools")]
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
                //TODO: Add validation for urns, category and dimension
                var urns = req.Query["urns"].ToString().Split(",");
                var category = req.Query["category"].ToString();
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.QuerySchoolsAsync(urns);
                return new JsonContentResult(result.Select(x => IncomeResponseFactory.Create(x, dimension, category)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query schools income");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryTrustsIncomeAsync))]
    [ProducesResponseType(typeof(TrustIncomeResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Income category", DataType = typeof(string), Required = true)]
    [QueryStringParameter("companyNumbers", "List of trust company numbers", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QueryTrustsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trusts")]
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
                //TODO: Add validation for companyNumbers, category and dimension
                var companyNumbers = req.Query["companyNumbers"].ToString().Split(",");
                var category = req.Query["category"].ToString();
                var dimension = req.Query["dimension"].ToString();
                var result = await _service.QueryTrustsAsync(companyNumbers);
                return new JsonContentResult(result.Select(x => IncomeResponseFactory.Create(x, dimension, category)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query trusts income");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}