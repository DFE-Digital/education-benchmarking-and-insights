using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IQuerySeniorLeadershipHandler : IVersionedHandler<BasicContext>;

public class QuerySeniorLeadershipV1Handler(ICensusService service, IValidator<SeniorLeadershipParameters> validator) : IQuerySeniorLeadershipHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<SeniorLeadershipParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.QuerySeniorLeadershipAsync(queryParams.Urns, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}