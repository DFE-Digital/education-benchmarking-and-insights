using System.Collections.Generic;
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

public class GetExpenditureTrustsFunction(IExpenditureService service, IValidator<ExpenditureQueryTrustParameters> validator)
{
    [Function(nameof(GetExpenditureTrustsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureTrustsFunction), Constants.Features.Expenditure)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleCategoryCost))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionFinance))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(IEnumerable<ExpenditureTrustResponse>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Trusts)] HttpRequestData req)
    {
        var queryParams = req.GetParameters<ExpenditureQueryTrustParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.QueryTrustsAsync(queryParams.CompanyNumbers, queryParams.Dimension);
        return await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category,
            queryParams.ExcludeCentralServices));
    }
}