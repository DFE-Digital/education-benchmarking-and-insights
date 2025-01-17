using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools;

public class SchoolsFunctions(
    ILogger<SchoolsFunctions> logger,
    ISchoolsService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleSchoolAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SingleSchoolAsync), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(School))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> SingleSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "school/{identifier}")]
        HttpRequestData req,
        string identifier)
    {
        var school = await service.GetAsync(identifier);

        return school == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(school);
    }

    [Function(nameof(SuggestSchoolsAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SuggestSchoolsAsync), "Schools")]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolSuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<School>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "schools/suggest")]
        HttpRequestData req)
    {
        var body = await req.ReadAsJsonAsync<SchoolSuggestRequest>();

        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var schools = await service.SuggestAsync(body);
        return await req.CreateJsonResponseAsync(schools);
    }
}