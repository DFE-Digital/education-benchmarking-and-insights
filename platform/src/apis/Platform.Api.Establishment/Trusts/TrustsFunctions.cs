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
using Platform.Api.Establishment.Schools;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;
namespace Platform.Api.Establishment.Trusts;

public class TrustsFunctions(
    ILogger<TrustsFunctions> logger,
    ITrustsService trustsService,
    ISchoolsService schoolsService,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleTrustAsync))]
    [OpenApiOperation(nameof(SingleTrustAsync), "Trusts")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleTrustAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{identifier}")] HttpRequestData req,
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
                var trust = await trustsService.GetAsync(identifier);
                if (trust == null)
                {
                    return req.CreateNotFoundResponse();
                }

                var schools = await schoolsService.QueryAsync(trust.CompanyNumber, null, null);
                var response = TrustResponseFactory.Create(trust, schools);
                return await req.CreateJsonResponseAsync(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SuggestTrustsAsync))]
    [OpenApiOperation(nameof(SuggestTrustsAsync), "Trusts")]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers to exclude", Type = typeof(string[]), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(SuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SuggestResponse<Trust>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/suggest")] HttpRequestData req)
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
                var trusts = await trustsService.SuggestAsync(body, numbers);
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