using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Requests;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Api.Benchmark.Shared;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Json;

namespace Platform.Api.Benchmark.Features.CustomData;

public class PostSchoolCustomDataFunction(ICustomDataService service)
{
    [Function(nameof(PostSchoolCustomDataFunction))]
    [OpenApiOperation(nameof(PostSchoolCustomDataFunction), Constants.Features.CustomData)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(CustomDataRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<MultiResponse> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = Routes.SchoolCustomData)] HttpRequestData req,
        string urn,
        CancellationToken cancellationToken = default)
    {
        var response = new MultiResponse();

        var body = await req.ReadAsJsonAsync<CustomDataRequest>(cancellationToken);
        var identifier = Guid.NewGuid().ToString();
        var data = body.CreateData(identifier, urn);

        await service.UpsertCustomDataAsync(data);
        await service.InsertNewAndDeactivateExistingUserDataAsync(CustomDataUserData.School(identifier, body.UserId, urn));

        var year = await service.CurrentYearAsync(cancellationToken);

        var message = new PipelineStartCustom
        {
            RunId = data.Id,
            RunType = Pipeline.RunType.Custom,
            Type = Pipeline.JobType.CustomData,
            URN = data.URN,
            Year = int.Parse(year),
            Payload = body.CreatePayload()
        };

        response.Messages = [message.ToJson()];
        response.HttpResponse = req.CreateResponse(HttpStatusCode.Accepted);
        return response;
    }
}