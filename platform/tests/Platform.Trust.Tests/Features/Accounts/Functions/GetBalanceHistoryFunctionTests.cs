using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Functions;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Accounts.Functions;

public class GetBalanceHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetBalanceHistoryFunction, IGetBalanceHistoryHandler, IdContext>
{
    protected override GetBalanceHistoryFunction CreateFunction(IEnumerable<IGetBalanceHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "companyNumber", CancellationToken.None];
}