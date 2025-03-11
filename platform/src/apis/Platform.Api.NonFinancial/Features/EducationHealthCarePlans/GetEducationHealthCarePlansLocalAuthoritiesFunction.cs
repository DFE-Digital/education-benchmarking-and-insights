using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;
using Platform.Api.NonFinancial.OpenApi.Examples;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans;

public class GetEducationHealthCarePlansLocalAuthoritiesFunction(
    IEducationHealthCarePlansService service,
    IValidator<EducationHealthCarePlansDimensionedParameters> validator)
{
    [Function(nameof(GetEducationHealthCarePlansLocalAuthoritiesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetEducationHealthCarePlansLocalAuthoritiesFunction), Constants.Features.HighNeeds)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Description = "List of local authority codes", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for resultant values", Type = typeof(string), Required = true, Example = typeof(ExampleEducationHealthCarePlansDimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityNumberOfPlans[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    public async Task<HttpResponseData> EducationHealthCarePlans(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorities)] HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<EducationHealthCarePlansDimensionedParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.Get(queryParams.Codes, queryParams.Dimension, token);
        return await req.CreateJsonResponseAsync(result);
    }
}