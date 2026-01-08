using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Functions;
using Platform.Api.Trust.Features.BudgetForecast.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.BudgetForecast.Functions;

public class QueryItSpendingFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryItSpendingFunction, IQueryItSpendingHandler, BasicContext>
{
    protected override QueryItSpendingFunction CreateFunction(IEnumerable<IQueryItSpendingHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}