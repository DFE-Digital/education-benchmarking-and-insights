using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Functions;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Census.Functions;

public class GetFunctionTests : FunctionRunAsyncReflectionTestsBase<GetFunction, IGetHandler, IdContext>
{
    protected override GetFunction CreateFunction(IEnumerable<IGetHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}