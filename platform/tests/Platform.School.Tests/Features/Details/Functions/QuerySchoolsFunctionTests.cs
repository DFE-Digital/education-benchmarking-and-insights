using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Functions;
using Platform.Api.School.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Details.Functions;

public class QuerySchoolsFunctionTests : FunctionRunAsyncReflectionTestsBase<QuerySchoolsFunction, IQuerySchoolsHandler, BasicContext>
{
    protected override QuerySchoolsFunction CreateFunction(IEnumerable<IQuerySchoolsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}