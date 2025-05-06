using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Services;
using Platform.Api.Benchmark.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.UserData;

public class GetUserDataFunction(IUserDataService service, IValidator<UserDataParameters> validator)
{
    [Function(nameof(GetUserDataFunction))]
    [OpenApiOperation(nameof(GetUserDataFunction), Constants.Features.UserData)]
    [OpenApiParameter("userId", In = ParameterLocation.Query, Description = "User Id as a Guid", Type = typeof(string), Required = true)]
    [OpenApiParameter("type", In = ParameterLocation.Query, Description = "Type", Type = typeof(string), Required = false, Example = typeof(ExampleUserDataType))]
    [OpenApiParameter("organisationType", In = ParameterLocation.Query, Description = "Organisation Type", Type = typeof(string), Required = false, Example = typeof(ExampleOrganisationType))]
    [OpenApiParameter("organisationId", In = ParameterLocation.Query, Description = "Organisation Id", Type = typeof(string), Required = false)]
    [OpenApiParameter("status", In = ParameterLocation.Query, Description = "Status", Type = typeof(string), Required = false, Example = typeof(ExampleUserDataStatus))]
    [OpenApiParameter("id", In = ParameterLocation.Query, Description = "Identifier", Type = typeof(string), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<Models.UserData>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.UserData)] HttpRequestData req)
    {
        var queryParams = req.GetParameters<UserDataParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var data = await service.QueryAsync(
            queryParams.UserId!,
            queryParams.Type,
            queryParams.Status,
            queryParams.Id,
            queryParams.OrganisationId,
            queryParams.OrganisationType);

        return await req.CreateJsonResponseAsync(data);
    }
}