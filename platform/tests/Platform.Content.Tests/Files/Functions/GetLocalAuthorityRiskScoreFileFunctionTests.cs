using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Content.Features.Files;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Content.Tests.Files.Functions;

public sealed class GetLocalAuthorityRiskScoreFileFunctionTests
    : FunctionRunAsyncReflectionTestsBase<GetLocalAuthorityRiskScoreFileFunction, IGetLocalAuthorityRiskScoreFileHandler, IdContext>
{
    protected override GetLocalAuthorityRiskScoreFileFunction CreateFunction(IEnumerable<IGetLocalAuthorityRiskScoreFileHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "2022", CancellationToken.None];
}
