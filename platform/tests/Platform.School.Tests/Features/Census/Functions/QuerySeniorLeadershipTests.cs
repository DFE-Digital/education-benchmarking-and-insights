using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Functions;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Census.Functions;

public class QuerySeniorLeadershipFunctionTests : FunctionRunAsyncReflectionTestsBase<QuerySeniorLeadershipFunction, IQuerySeniorLeadershipHandler, BasicContext>
{
    protected override QuerySeniorLeadershipFunction CreateFunction(IEnumerable<IQuerySeniorLeadershipHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}