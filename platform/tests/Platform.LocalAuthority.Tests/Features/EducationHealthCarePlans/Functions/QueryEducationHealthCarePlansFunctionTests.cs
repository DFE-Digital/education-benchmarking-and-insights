using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Functions;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.EducationHealthCarePlans.Functions;

public sealed class QueryEducationHealthCarePlansFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryEducationHealthCarePlansFunction, IQueryEducationHealthCarePlansHandler, BasicContext>
{
    protected override QueryEducationHealthCarePlansFunction CreateFunction(IEnumerable<IQueryEducationHealthCarePlansHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}