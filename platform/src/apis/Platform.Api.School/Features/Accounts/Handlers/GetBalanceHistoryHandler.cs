using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetBalanceHistoryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetBalanceHistoryV1Handler(IBalanceService service) : IGetBalanceHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<BalanceParameters>();

        var (years, rows) = await service.GetHistoryAsync(identifier, queryParams.Dimension, cancellationToken);
        return years == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}