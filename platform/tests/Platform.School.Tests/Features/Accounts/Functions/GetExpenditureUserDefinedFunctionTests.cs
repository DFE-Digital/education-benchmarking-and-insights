using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetExpenditureUserDefinedFunctionTests : FunctionRunAsyncReflectionTestsBase<GetExpenditureUserDefinedFunction, IGetExpenditureUserDefinedHandler, IdPairContext>
{
    protected override GetExpenditureUserDefinedFunction CreateFunction(IEnumerable<IGetExpenditureUserDefinedHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", "id", CancellationToken.None];
}