using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Requests;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class GetLocalAuthoritiesSuggestFunction(ILocalAuthoritiesService service, IValidator<SuggestRequest> validator)
{
    [Function(nameof(GetLocalAuthoritiesSuggestFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthoritiesSuggestFunction), Constants.Features.LocalAuthorities)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(LocalAuthoritySuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<LocalAuthority>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.LocalAuthoritiesSuggest)]
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