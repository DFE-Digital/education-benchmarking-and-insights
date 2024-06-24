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

namespace Platform.Api.Insight.Expenditure;

[ApiExplorerSettings(GroupName = "Expenditure")]
public class ExpenditureFunctions
{

    private readonly ILogger<ExpenditureFunctions> _logger;
    private readonly IExpenditureService _service;

    public ExpenditureFunctions(ILogger<ExpenditureFunctions> logger, IExpenditureService service)
    {
        _logger = logger;
        _service = service;
    }

    [FunctionName(nameof(ExpenditureAllCategories))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult ExpenditureAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/categories")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(ExpenditureCategories.All);
        }
    }


    [FunctionName(nameof(ExpenditureAllDimensions))]
    [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.OK)]
    public IActionResult ExpenditureAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/dimensions")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {

            return new JsonContentResult(ExpenditureDimensions.All);
        }
    }

    [FunctionName(nameof(SchoolExpenditureAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Expenditure category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> SchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var result = await _service.GetSchoolAsync(urn);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(ExpenditureResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school expenditure");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CustomSchoolExpenditureAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Expenditure category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> CustomSchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/custom/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var result = await _service.GetCustomSchoolAsync(urn, identifier);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(ExpenditureResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get custom school expenditure");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(TrustExpenditureAsync))]
    [ProducesResponseType(typeof(TrustExpenditureResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Expenditure category", DataType = typeof(string))]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string))]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> TrustExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trust/{companyNumber}")]
        HttpRequest req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                var result = await _service.GetTrustAsync(companyNumber);
                return result == null
                    ? new NotFoundResult()
                    : new JsonContentResult(ExpenditureResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get expenditure");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolExpenditureHistoryAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> SchoolExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/history")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for dimension
                var result = await _service.GetSchoolHistoryAsync(urn);
                return new JsonContentResult(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school expenditure history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustExpenditureHistoryAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureHistoryResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("dimension", "Dimension for response values", DataType = typeof(string), Required = true)]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> TrustExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trust/{companyNumber}/history")]
        HttpRequest req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for dimension
                var result = await _service.GetTrustHistoryAsync(companyNumber);
                return new JsonContentResult(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust expenditure history");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsExpenditureAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Expenditure category", DataType = typeof(string))]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string))]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> QuerySchoolsExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/schools")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for urns, category and dimension
                var result = await _service.QuerySchoolsAsync(queryParams.Schools);
                return new JsonContentResult(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query schools expenditure");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryTrustsExpenditureAsync))]
    [ProducesResponseType(typeof(TrustExpenditureResponse[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("category", "Expenditure category", DataType = typeof(string))]
    [QueryStringParameter("companyNumbers", "List of trust company numbers", DataType = typeof(string[]))]
    [QueryStringParameter("dimension", "Value dimension", DataType = typeof(string))]
    [QueryStringParameter("excludeCentralServices", "Exclude central services amounts", DataType = typeof(bool), Required = false)]
    public async Task<IActionResult> QueryTrustsExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trusts")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                //TODO: Add validation for companyNumbers, category and dimension
                var result = await _service.QueryTrustsAsync(queryParams.Trusts);
                return new JsonContentResult(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query trusts expenditure");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}