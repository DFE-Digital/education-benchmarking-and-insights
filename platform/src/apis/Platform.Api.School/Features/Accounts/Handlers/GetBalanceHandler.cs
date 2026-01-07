using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetBalanceHandler : IVersionedHandler<IdContext>;

public class GetBalanceV1Handler(IBalanceService service) : IGetBalanceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var result = await service.GetAsync(context.Id, context.Token);
        return result == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(), context.Token);
    }
}