using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts;

public class TrustComparatorsFunctions(ILogger<TrustComparatorsFunctions> logger, ITrustComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(TrustComparatorsAsync))]
    [OpenApiOperation(nameof(TrustComparatorsAsync), Constants.Features.Trusts)]
    [OpenApiSecurityHeader]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustComparators))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "trust/{identifier}/comparators")] HttpRequestData req,
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
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<TrustComparatorsRequest>();
                var comparators = await service.ComparatorsAsync(identifier, body);
                return await req.CreateJsonResponseAsync(comparators);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to create trust comparators");
                return req.CreateErrorResponse();
            }
        }
    }
}