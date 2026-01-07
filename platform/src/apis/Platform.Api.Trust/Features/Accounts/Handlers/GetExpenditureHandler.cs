using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IGetExpenditureHandler : IVersionedHandler<IdContext>;

public class GetExpenditureV1Handler(IAccountsService service, IValidator<ExpenditureParameters> validator) : IGetExpenditureHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.GetExpenditureAsync(context.Id, queryParams.Dimension, context.Token);
        return result == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), context.Token);
    }
}