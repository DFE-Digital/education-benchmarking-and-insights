using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Trusts;

public class GetTrustsSuggestFunction(ITrustsService service, IValidator<SuggestRequest> validator)
{
    [Function(nameof(GetTrustsSuggestFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustsSuggestFunction), Constants.Features.Trusts)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustSuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<Trust>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = Routes.TrustsSuggest)]
        HttpRequestData req)
    {
        var body = await req.ReadAsJsonAsync<TrustSuggestRequest>();

        var validationResult = await validator.ValidateAsync(body);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var trusts = await service.SuggestAsync(body);
        return await req.CreateJsonResponseAsync(trusts);
    }
}