using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetBalanceHistoryHandler : IVersionedHandler<IdContext>;

public class GetBalanceHistoryV1Handler(IBalanceService service) : IGetBalanceHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<BalanceParameters>();

        var (years, rows) = await service.GetHistoryAsync(context.Id, queryParams.Dimension, context.Token);
        return years == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}