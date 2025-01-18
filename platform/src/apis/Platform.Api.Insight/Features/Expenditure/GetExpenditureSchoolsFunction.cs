using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Responses;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Expenditure;

public class GetExpenditureSchoolsFunction(IExpenditureService service, IValidator<ExpenditureQuerySchoolParameters> validator)
{
    [Function(nameof(GetExpenditureSchoolsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureSchoolsFunction), Constants.Features.Expenditure)]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleCategoryCost))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureSchoolResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Schools)]
        HttpRequestData req)
    {
        var queryParams = req.GetParameters<ExpenditureQuerySchoolParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.QuerySchoolsAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode,
            queryParams.Phase, queryParams.Dimension);
        return await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
    }
}