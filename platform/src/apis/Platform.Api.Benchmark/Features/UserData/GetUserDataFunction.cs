using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Services;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.Benchmark.Features.UserData;

public class GetUserDataFunction(IUserDataService service, IValidator<UserDataParameters> validator)
{
    [Function(nameof(GetUserDataFunction))]
    [OpenApiOperation(nameof(GetUserDataFunction), Constants.Features.UserData)]
    [OpenApiParameter("userId", In = ParameterLocation.Query, Description = "User Id as a Guid", Type = typeof(string), Required = true)]
    [OpenApiParameter("type", In = ParameterLocation.Query, Description = "Type", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.UserDataType))]
    [OpenApiParameter("organisationType", In = ParameterLocation.Query, Description = "Organisation Type", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.OrganisationType))]
    [OpenApiParameter("organisationId", In = ParameterLocation.Query, Description = "Organisation Id", Type = typeof(string), Required = false)]
    [OpenApiParameter("status", In = ParameterLocation.Query, Description = "Status", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.UserDataStatus))]
    [OpenApiParameter("id", In = ParameterLocation.Query, Description = "Identifier", Type = typeof(string), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<Models.UserData>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.UserData)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<UserDataParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var data = await service.QueryAsync(
            queryParams.UserId!,
            queryParams.Type,
            queryParams.Status,
            queryParams.Id,
            queryParams.OrganisationId,
            queryParams.OrganisationType,
            cancellationToken);

        return await req.CreateJsonResponseAsync(data, cancellationToken);
    }
}
