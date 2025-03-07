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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans;

public class GetEducationHealthCarePlansLocalAuthoritiesFunction(
    IEducationHealthCarePlansService service,
    IValidator<EducationHealthCarePlansParameters> validator)
{
    [Function(nameof(GetEducationHealthCarePlansLocalAuthoritiesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetEducationHealthCarePlansLocalAuthoritiesFunction), Constants.Features.HighNeeds)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Description = "List of local authority codes", Type = typeof(string[]), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityNumberOfPlans[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    public async Task<HttpResponseData> EducationHealthCarePlans(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorities)] HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<EducationHealthCarePlansParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.Get(queryParams.Codes, token);
        return await req.CreateJsonResponseAsync(result);
    }
}