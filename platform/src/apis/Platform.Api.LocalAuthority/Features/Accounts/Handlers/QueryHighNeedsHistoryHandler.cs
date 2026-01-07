using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Accounts.Handlers;

public interface IQueryHighNeedsHistoryHandler : IVersionedHandler<BasicContext>;

public class QueryHighNeedsHistoryV1Handler(IHighNeedsService service, IValidator<HighNeedsParameters> validator) : IQueryHighNeedsHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<HighNeedsParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var history = await service.QueryHistoryAsync(queryParams.Codes, queryParams.Dimension, context.Token);
        return history == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(history, context.Token);
    }
}