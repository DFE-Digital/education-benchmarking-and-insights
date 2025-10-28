using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.OpenApi.Models;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Responses;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.LocalAuthorityFinances.Features.Schools;

public class GetWorkforceSummaryFunction(ISchoolsService service, IValidator<WorkforceSummaryParameters> validator)
{
    [Function(nameof(GetWorkforceSummaryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetWorkforceSummaryFunction), Constants.Features.Schools)]
    [OpenApiParameter("code", Type = typeof(string), Required = true)]
    // TODO: Add example for dimension
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for resultant values", Type = typeof(string))]
    [OpenApiParameter("nurseryProvision", In = ParameterLocation.Query, Description = "List of nursery provisions to filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("sixthFormProvision", In = ParameterLocation.Query, Description = "List of sixth provisions filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("specialClassesProvision", In = ParameterLocation.Query, Description = "List of special class provisions filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("overallPhase", In = ParameterLocation.Query, Description = "Phase to filter resultant values", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("sortField", In = ParameterLocation.Query, Description = "Field to sort by", Type = typeof(string), Required = false)]
    [OpenApiParameter("sortOrder", In = ParameterLocation.Query, Description = "Sort direction: 'asc' or 'desc'", Type = typeof(string), Required = false)]
    [OpenApiParameter("limit", In = ParameterLocation.Query, Description = "Number of records to return if empty all are returned", Type = typeof(string), Required = false)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(WorkforceSummaryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Workforce)] HttpRequestData req,
        string code,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<WorkforceSummaryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        int? parsedLimit = int.TryParse(queryParams.Limit, out var parsed) ? parsed : null;


        var result = await service.GetWorkforceSummaryAsync(
            code,
            queryParams.Dimension,
            queryParams.NurseryProvision,
            queryParams.SixthFormProvision,
            queryParams.SpecialClassesProvision,
            parsedLimit,
            queryParams.SortField,
            queryParams.SortOrder,
            queryParams.OverallPhase,
            cancellationToken: cancellationToken);

        return result.IsEmpty()
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result, cancellationToken);
    }
}