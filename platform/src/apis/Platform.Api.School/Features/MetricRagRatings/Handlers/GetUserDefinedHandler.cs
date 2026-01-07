using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Parameters;
using Platform.Api.School.Features.MetricRagRatings.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.MetricRagRatings.Handlers;

public interface IGetUserDefinedHandler : IVersionedHandler<IdContext>;

public class GetUserDefinedV1Handler(IMetricRagRatingsService service) : IGetUserDefinedHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<UserDefinedParameters>();

        var result = await service.GetUserDefinedAsync(context.Id, queryParams.DataContext, cancellationToken: context.Token);
        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}