using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IQueryHandler : IVersionedHandler<BasicContext>;

public class QueryV1Handler(ICensusService service, IValidator<QueryCensusParameters> validator) : IQueryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<QueryCensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), context.Token);
    }
}