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

namespace Platform.Api.Establishment.Features.Trusts;

public class TrustsFunctions(
    ITrustsService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleTrustAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SingleTrustAsync), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Trust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> SingleTrustAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "trust/{identifier}")]
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

    [Function(nameof(SuggestTrustsAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SuggestTrustsAsync), Constants.Features.Trusts)]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(TrustSuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<Trust>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "trusts/suggest")]
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