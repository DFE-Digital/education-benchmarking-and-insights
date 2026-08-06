using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Risks.Functions;
using Platform.Api.LocalAuthority.Features.Risks.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.Risks.Functions;

public sealed class QueryPagedSchoolRisksFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryPagedSchoolRisksFunction, IQueryPagedSchoolRisksHandler, BasicContext>
{
    protected override QueryPagedSchoolRisksFunction CreateFunction(IEnumerable<IQueryPagedSchoolRisksHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}
