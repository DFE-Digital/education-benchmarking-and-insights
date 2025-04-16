using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Api.Establishment.Features.Trusts.Validators;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Trusts;

public class PostTrustsSearchFunction(
    ITrustsService service,
    [FromKeyedServices(nameof(TrustsSearchValidator))] IValidator<SearchRequest> validator)
{
    [Function(nameof(PostTrustsSearchFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostTrustsSearchFunction), "Trusts")]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SearchRequest), Description = "The search request")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SearchResponse<TrustSummary>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.TrustsSearch)]
        HttpRequestData req)
    {
        var body = await req.ReadAsJsonAsync<SearchRequest>();

        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var trusts = await service.TrustsSearchAsync(body);
        return await req.CreateJsonResponseAsync(trusts);
    }
}