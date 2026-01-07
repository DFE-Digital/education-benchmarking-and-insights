using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Functions;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.Details.Functions;

public sealed class QueryMaintainedSchoolsWorkforceFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryMaintainedSchoolsWorkforceFunction, IQueryMaintainedSchoolWorkforceHandler, IdContext>
{
    protected override QueryMaintainedSchoolsWorkforceFunction CreateFunction(IEnumerable<IQueryMaintainedSchoolWorkforceHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "code", CancellationToken.None];
}