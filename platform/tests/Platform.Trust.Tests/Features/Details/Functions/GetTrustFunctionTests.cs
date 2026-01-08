using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Details.Functions;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Details.Functions;

public class GetTrustFunctionTests : FunctionRunAsyncReflectionTestsBase<GetTrustFunction, IGetTrustHandler, IdContext>
{
    protected override GetTrustFunction CreateFunction(IEnumerable<IGetTrustHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "companyNumber", CancellationToken.None];
}