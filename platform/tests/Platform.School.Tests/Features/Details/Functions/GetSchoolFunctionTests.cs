using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Functions;
using Platform.Api.School.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Details.Functions;

public class GetSchoolFunctionTests : FunctionRunAsyncReflectionTestsBase<GetSchoolFunction, IGetSchoolHandler, IdContext>
{
    protected override GetSchoolFunction CreateFunction(IEnumerable<IGetSchoolHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}