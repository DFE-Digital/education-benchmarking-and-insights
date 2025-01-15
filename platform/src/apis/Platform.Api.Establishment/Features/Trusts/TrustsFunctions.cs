using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search.Requests;
using Platform.Search.Responses;

namespace Platform.Api.Establishment.Features.Trusts;

public class TrustsFunctions(
    ILogger<TrustsFunctions> logger,
    ITrustsService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleTrustAsync))]
    [OpenApiOperation(nameof(SingleTrustAsync), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Trust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleTrustAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "trust/{identifier}")] HttpRequestData req,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var response = await service.GetAsync(identifier);
                if (response == null)
                {
                    return req.CreateNotFoundResponse();
                }

                return await req.CreateJsonResponseAsync(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust");
                return req.CreateErrorResponse();
            }
        }
    }

    //TODO: Create specific suggest request
    //TODO: Add query parameter to new request
    [Function(nameof(SuggestTrustsAsync))]
    [OpenApiOperation(nameof(SuggestTrustsAsync), Constants.Features.Trusts)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers to exclude", Type = typeof(string[]), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<Trust>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "trusts/suggest")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<SuggestRequest>();

                var validationResult = await validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var numbers = req.Query["companyNumbers"]?.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var trusts = await service.SuggestAsync(body, numbers);
                return await req.CreateJsonResponseAsync(trusts);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust suggestions");
                return req.CreateErrorResponse();
            }
        }
    }
}