using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Functions.OpenApi;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans;

public class GetEducationHealthCarePlansFunction(
    ILogger<GetEducationHealthCarePlansFunction> logger,
    IEducationHealthCarePlansService service,
    IValidator<EducationHealthCarePlansParameters> validator)
{
    [Function(nameof(GetEducationHealthCarePlansFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetEducationHealthCarePlansFunction), Constants.Features.HighNeeds)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Description = "List of local authority codes", Type = typeof(string[]), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(History<LocalAuthorityNumberOfPlansYear>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorityEducationHealthCarePlans)] HttpRequestData req,
        CancellationToken token)
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
                var queryParams = req.GetParameters<EducationHealthCarePlansParameters>();

                var validationResult = await validator.ValidateAsync(queryParams, token);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.GetHistory(queryParams.Codes);
                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get education health care plans");
                return req.CreateErrorResponse();
            }
        }
    }
}