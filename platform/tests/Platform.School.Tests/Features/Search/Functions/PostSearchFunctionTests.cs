using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Search.Functions;
using Platform.Api.School.Features.Search.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Search.Functions;

public class PostSearchFunctionTests : FunctionRunAsyncReflectionTestsBase<PostSearchFunction, IPostSearchHandler, BasicContext>
{
    protected override PostSearchFunction CreateFunction(IEnumerable<IPostSearchHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}