using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class PostLocalAuthoritiesSearchFunction(
    ILocalAuthoritiesService service,
    [FromKeyedServices(nameof(LocalAuthoritiesFeature))] IValidator<SearchRequest> validator)
{
    [Function(nameof(PostLocalAuthoritiesSearchFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(PostLocalAuthoritiesSearchFunction), Constants.Features.LocalAuthorities)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SearchRequest), Description = "The search request")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SearchResponse<LocalAuthoritySummary>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.LocalAuthoritiesSearch)] HttpRequestData req)
    {
        var body = await req.ReadAsJsonAsync<SearchRequest>();

        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var localAuthorities = await service.LocalAuthoritiesSearchAsync(body);
        return await req.CreateJsonResponseAsync(localAuthorities);
    }
}