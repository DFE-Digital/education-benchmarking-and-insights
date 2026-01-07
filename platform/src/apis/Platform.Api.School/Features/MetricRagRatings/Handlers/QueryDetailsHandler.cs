using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Api.School.Features.MetricRagRatings.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.MetricRagRatings.Handlers;

public interface IQueryDetailsHandler : IVersionedHandler<BasicContext>;

public class QueryDetailsV1Handler(IMetricRagRatingsService service, IValidator<DetailParameters> validator) : IQueryDetailsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<DetailParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.QueryDetailAsync(queryParams.Urns, queryParams.Categories, queryParams.Statuses, queryParams.CompanyNumber, cancellationToken: context.Token);
        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}