using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Comparators.Functions;
using Platform.Api.Trust.Features.Comparators.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Comparators.Functions;

public class PostComparatorsFunctionTests : FunctionRunAsyncReflectionTestsBase<PostComparatorsFunction, IPostComparatorsHandler, IdContext>
{
    protected override PostComparatorsFunction CreateFunction(IEnumerable<IPostComparatorsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "companyNumber", CancellationToken.None];
}