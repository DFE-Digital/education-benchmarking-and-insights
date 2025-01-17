using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class LocalAuthoritiesFunctions(
    ILocalAuthoritiesService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleLocalAuthorityAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SingleLocalAuthorityAsync), Constants.Features.LocalAuthorities)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthority))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> SingleLocalAuthorityAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "local-authority/{identifier}")]
        HttpRequestData req,
        string identifier)
    {
        var response = await service.GetAsync(identifier);
        if (response == null)
        {
            return req.CreateNotFoundResponse();
        }

        return await req.CreateJsonResponseAsync(response);
    }

    [Function(nameof(SuggestLocalAuthoritiesAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SuggestLocalAuthoritiesAsync), Constants.Features.LocalAuthorities)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(LocalAuthoritySuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<LocalAuthority>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "local-authorities/suggest")]
        HttpRequestData req)
    {
        var body = await req.ReadAsJsonAsync<LocalAuthoritySuggestRequest>();

        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var localAuthorities = await service.SuggestAsync(body);
        return await req.CreateJsonResponseAsync(localAuthorities);
    }
}