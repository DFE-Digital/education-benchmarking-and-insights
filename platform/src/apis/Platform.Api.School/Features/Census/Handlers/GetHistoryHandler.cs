using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IGetHistoryHandler : IVersionedHandler<IdContext>;

public class GetHistoryV1Handler(ICensusService service, IValidator<GetParameters> validator) : IGetHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<GetParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var (years, rows) = await service.GetHistoryAsync(context.Id, queryParams.Dimension, context.Token);
        return years == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}