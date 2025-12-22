using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Search.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Handlers;

public interface IPostSearchHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class PostSearchV1Handler(ILocalAuthoritySearchService service, [FromKeyedServices(Constants.Features.Search)] IValidator<SearchRequest> validator) : IPostSearchHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var body = await request.ReadAsJsonAsync<SearchRequest>(cancellationToken);

        var validationResult = await validator.ValidateAsync(body, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken: cancellationToken);
        }

        var trusts = await service.SearchAsync(body, cancellationToken);
        return await request.CreateJsonResponseAsync(trusts, cancellationToken);
    }
}