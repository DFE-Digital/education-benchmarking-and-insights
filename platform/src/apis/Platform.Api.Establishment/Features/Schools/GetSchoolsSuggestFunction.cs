using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools;

public class GetSchoolsSuggestFunction(ISchoolsService service, IValidator<SuggestRequest> validator)
{
    [Function(nameof(GetSchoolsSuggestFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolsSuggestFunction), "Schools")]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SchoolSuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<School>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.SchoolsSuggest)]
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