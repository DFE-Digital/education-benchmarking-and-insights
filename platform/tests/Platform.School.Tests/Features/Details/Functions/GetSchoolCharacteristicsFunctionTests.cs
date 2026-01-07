using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Functions;
using Platform.Api.School.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Details.Functions;

public class GetSchoolCharacteristicsFunctionTests: FunctionRunAsyncReflectionTestsBase<GetSchoolCharacteristicsFunction, IGetSchoolCharacteristicsHandler, IdContext>
{
    protected override GetSchoolCharacteristicsFunction CreateFunction(IEnumerable<IGetSchoolCharacteristicsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}