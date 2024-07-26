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
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Infrastructure.Search;
namespace Platform.Api.Establishment.LocalAuthorities;

public class LocalAuthoritiesFunctions(ILogger<LocalAuthoritiesFunctions> logger,
    ILocalAuthoritiesService service, IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleLocalAuthorityAsync))]
    [OpenApiOperation(nameof(SingleLocalAuthorityAsync), "Local Authorities")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(LocalAuthority))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleLocalAuthorityAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authority/{identifier}")] HttpRequestData req,
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

                return response == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(response);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get local authority");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SuggestLocalAuthoritiesAsync))]
    [OpenApiOperation(nameof(SingleLocalAuthorityAsync), "Local Authorities")]
    [OpenApiParameter("names", In = ParameterLocation.Query, Description = "List of LA names to exclude", Type = typeof(string[]))]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(SuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SuggestResponse<LocalAuthority>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/suggest")] HttpRequestData req)
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

                var names = req.Query["names"]?.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var localAuthorities = await service.SuggestAsync(body, names);
                return await req.CreateJsonResponseAsync(localAuthorities);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get local authority suggestions");
                return req.CreateErrorResponse();
            }
        }
    }
}