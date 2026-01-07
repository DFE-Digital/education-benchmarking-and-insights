using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetExpenditureUserDefinedHandler : IVersionedHandler<IdPairContext>;

public class GetExpenditureUserDefinedV1Handler(IExpenditureService service, IValidator<ExpenditureParameters> validator) : IGetExpenditureUserDefinedHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdPairContext context)
    {
        var queryParams = context.Request.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.GetUserDefinedAsync(context.Id1, context.Id2, queryParams.Dimension, context.Token);
        return result == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), context.Token);
    }
}