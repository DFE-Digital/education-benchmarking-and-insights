using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Search.Models;
using Platform.Api.Trust.Features.Search.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search.Handlers;

public interface IPostSuggestHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class PostSuggestV1Handler(ITrustSearchService service, IValidator<SuggestRequest> validator) : IPostSuggestHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var body = await request.ReadAsJsonAsync<TrustSuggestRequest>(cancellationToken);

        var validationResult = await validator.ValidateAsync(body, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken: cancellationToken);
        }

        var trusts = await service.SuggestAsync(body, cancellationToken);
        return await request.CreateJsonResponseAsync(trusts, cancellationToken);
    }
}