using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions;

namespace Platform.Api.Establishment.Features.Schools;

public class SchoolComparatorsFunctions(ILogger<SchoolComparatorsFunctions> logger, ISchoolComparatorsService service)
{
    //TODO : Consider request validation
    [Function(nameof(SchoolComparatorsAsync))]
    [OpenApiOperation(nameof(SchoolComparatorsAsync), Constants.Features.Schools)]
    [OpenApiSecurityHeader]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolComparatorsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolComparators))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "school/{identifier}/comparators")] HttpRequestData req,
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
                var body = await req.ReadAsJsonAsync<SchoolComparatorsRequest>();
                var comparators = await service.ComparatorsAsync(identifier, body);
                return await req.CreateJsonResponseAsync(comparators);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to create school comparators");
                return req.CreateErrorResponse();
            }
        }
    }
}