using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Search.Models;
using Platform.Api.LocalAuthority.Features.Search.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search.Handlers;

public interface IPostSuggestHandler : IVersionedHandler<BasicContext>;

public class PostSuggestV1Handler(ILocalAuthoritySearchService service, IValidator<SuggestRequest> validator) : IPostSuggestHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var body = await context.Request.ReadAsJsonAsync<LocalAuthoritySuggestRequest>(context.Token);

        var validationResult = await validator.ValidateAsync(body, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var trusts = await service.SuggestAsync(body, context.Token);
        return await context.Request.CreateJsonResponseAsync(trusts, context.Token);
    }
}