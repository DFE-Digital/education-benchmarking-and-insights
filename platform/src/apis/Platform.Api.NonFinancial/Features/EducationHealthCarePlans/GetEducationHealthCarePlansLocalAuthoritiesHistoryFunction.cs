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

public class GetEducationHealthCarePlansLocalAuthoritiesHistoryFunction(
    IEducationHealthCarePlansService service,
    IValidator<EducationHealthCarePlansParameters> validator)
{
    [Function(nameof(GetEducationHealthCarePlansLocalAuthoritiesHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetEducationHealthCarePlansLocalAuthoritiesHistoryFunction), Constants.Features.HighNeeds)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Description = "List of local authority codes", Type = typeof(string[]), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(History<LocalAuthorityNumberOfPlansYear>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    public async Task<HttpResponseData> EducationHealthCarePlans(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthoritiesHistory)] HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<EducationHealthCarePlansParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.GetHistory(queryParams.Codes, token);
        return await req.CreateJsonResponseAsync(result);
    }
}