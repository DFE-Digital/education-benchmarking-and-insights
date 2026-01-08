using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Functions;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Accounts.Functions;

public class GetExpenditureFunctionTests : FunctionRunAsyncReflectionTestsBase<GetExpenditureFunction, IGetExpenditureHandler, IdContext>
{
    protected override GetExpenditureFunction CreateFunction(IEnumerable<IGetExpenditureHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "companyNumber", CancellationToken.None];
}