﻿using System;
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

namespace Platform.Api.Insight.MetricRagRatings;

[ApiExplorerSettings(GroupName = "Metric RAG Ratings")]
public class MetricRagRatingsFunctions
{
    private readonly ILogger<MetricRagRatingsFunctions> _logger;
    private readonly IMetricRagRatingsService _service;

    public MetricRagRatingsFunctions(IMetricRagRatingsService service, ILogger<MetricRagRatingsFunctions> logger)
    {
        _service = service;
        _logger = logger;
    }

    [FunctionName(nameof(UserDefinedAsync))]
    [ProducesResponseType(typeof(MetricRagRating[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("useCustomData", "Sets whether or not to use custom data context", DataType = typeof(bool), Required = false)]

    public async Task<IActionResult> UserDefinedAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "metric-rag/{identifier}")] HttpRequest req,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<MetricRagRatingParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   {"Application", Constants.ApplicationName},
                   {"CorrelationID", correlationId},
                   {"Identifier", identifier}
               }))
        {
            try
            {
                var result = await _service.UserDefinedAsync(identifier, queryParams.DataContext);

                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed get user defined metric rag ratings");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryDefaultAsync))]
    [ProducesResponseType(typeof(MetricRagRating[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string[]), Required = true)]
    [QueryStringParameter("categories", "List of cost category", DataType = typeof(string[]), Required = false)]
    [QueryStringParameter("statuses", "List of RAG statuses", DataType = typeof(string[]), Required = false)]
    public async Task<IActionResult> QueryDefaultAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "metric-rag/default")] HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<MetricRagRatingsParameters>();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   {"Application", Constants.ApplicationName},
                   {"CorrelationID", correlationId}
               }))
        {
            try
            {
                var result = await _service.QueryAsync(queryParams.Schools, queryParams.Categories, queryParams.Statuses);
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed query metric rag ratings");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}