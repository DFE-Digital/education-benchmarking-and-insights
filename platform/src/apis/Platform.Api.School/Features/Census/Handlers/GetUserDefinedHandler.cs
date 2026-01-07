using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IGetUserDefinedHandler : IVersionedHandler<IdPairContext>;

public class GetUserDefinedV1Handler(ICensusService service, IValidator<GetParameters> validator) : IGetUserDefinedHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdPairContext context)
    {
        var queryParams = context.Request.GetParameters<GetParameters>();

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