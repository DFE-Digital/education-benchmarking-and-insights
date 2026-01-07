using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Accounts.Handlers;

public interface IQueryHighNeedsHandler : IVersionedHandler<BasicContext>;

public class QueryHighNeedsV1Handler(IHighNeedsService service, IValidator<HighNeedsParameters> validator) : IQueryHighNeedsHandler
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

        var highNeeds = await service.QueryAsync(queryParams.Codes, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(highNeeds, context.Token);
    }
}