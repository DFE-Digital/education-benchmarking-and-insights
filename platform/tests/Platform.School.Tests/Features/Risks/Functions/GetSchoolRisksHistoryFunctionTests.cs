using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Risks.Functions;
using Platform.Api.School.Features.Risks.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Risks.Functions;

public sealed class GetSchoolRisksHistoryFunctionTests
    : FunctionRunAsyncReflectionTestsBase<GetSchoolRisksHistoryFunction, IGetSchoolRisksHistoryHandler, IdContext>
{
    protected override GetSchoolRisksHistoryFunction CreateFunction(IEnumerable<IGetSchoolRisksHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "123456", CancellationToken.None];
}
