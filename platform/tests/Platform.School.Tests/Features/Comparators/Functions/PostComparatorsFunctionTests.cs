using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Comparators.Functions;
using Platform.Api.School.Features.Comparators.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Comparators.Functions;

public class PostComparatorsFunctionTests : FunctionRunAsyncReflectionTestsBase<PostComparatorsFunction, IPostComparatorsHandler, IdContext>
{
    protected override PostComparatorsFunction CreateFunction(IEnumerable<IPostComparatorsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}