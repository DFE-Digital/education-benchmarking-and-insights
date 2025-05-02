using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Parameters;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class GetLocalAuthoritiesNationalRankFunction(ILocalAuthorityRankingService service, IValidator<LocalAuthoritiesNationalRankParameters> validator)
{
    [Function(nameof(GetLocalAuthoritiesNationalRankFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthoritiesNationalRankFunction), Constants.Features.LocalAuthorities)]
    [OpenApiParameter("ranking", In = ParameterLocation.Query, Description = "Type of national ranking", Type = typeof(string), Required = true, Example = typeof(ExampleLocalAuthorityNationalRanking))]
    [OpenApiParameter("sort", In = ParameterLocation.Query, Description = "Sort order for ranking", Type = typeof(string), Required = false, Example = typeof(ExampleSort))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityRanking))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthoritiesNationalRank)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<LocalAuthoritiesNationalRankParameters>();
        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var response = await service.GetRanking(queryParams.Ranking, queryParams.Sort, cancellationToken);
        return await req.CreateJsonResponseAsync(response, cancellationToken: cancellationToken);
    }
}