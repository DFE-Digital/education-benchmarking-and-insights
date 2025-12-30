using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Api.School.Features.Accounts.Validators;
using Platform.Functions;

namespace Platform.Api.School.Features.Accounts;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddAccountsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IQueryItSpendingHandler, QueryItSpendingV1Handler>()
            .AddSingleton<IGetIncomeHandler, GetIncomeV1Handler>()
            .AddSingleton<IGetIncomeHistoryHandler, GetIncomeHistoryV1Handler>()
            .AddSingleton<IGetBalanceHandler, GetBalanceV1Handler>()
            .AddSingleton<IGetBalanceHistoryHandler, GetBalanceHistoryV1Handler>()
            .AddSingleton<IGetExpenditureComparatorSetAverageHistoryHandler, GetExpenditureComparatorSetAverageHistoryV1Handler>()
            .AddSingleton<IGetExpenditureHandler, GetExpenditureV1Handler>()
            .AddSingleton<IGetExpenditureHistoryHandler, GetExpenditureHistoryV1Handler>()
            .AddSingleton<IGetExpenditureNationalAverageHistoryHandler, GetExpenditureNationalAverageHistoryV1Handler>()
            .AddSingleton<IGetExpenditureUserDefinedHandler, GetExpenditureUserDefinedV1Handler>()
            .AddSingleton<IQueryExpenditureHandler, QueryExpenditureV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryItSpendingHandler>, VersionedHandlerDispatcher<IQueryItSpendingHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetIncomeHandler>, VersionedHandlerDispatcher<IGetIncomeHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetIncomeHistoryHandler>, VersionedHandlerDispatcher<IGetIncomeHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetBalanceHandler>, VersionedHandlerDispatcher<IGetBalanceHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetBalanceHistoryHandler>, VersionedHandlerDispatcher<IGetBalanceHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureComparatorSetAverageHistoryHandler>, VersionedHandlerDispatcher<IGetExpenditureComparatorSetAverageHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureHandler>, VersionedHandlerDispatcher<IGetExpenditureHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureHistoryHandler>, VersionedHandlerDispatcher<IGetExpenditureHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureNationalAverageHistoryHandler>, VersionedHandlerDispatcher<IGetExpenditureNationalAverageHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureUserDefinedHandler>, VersionedHandlerDispatcher<IGetExpenditureUserDefinedHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryExpenditureHandler>, VersionedHandlerDispatcher<IQueryExpenditureHandler>>()
            .AddTransient<IValidator<ItSpendingParameters>, ItSpendingParametersValidator>()
            .AddTransient<IValidator<IncomeParameters>, IncomeParametersValidator>()
            .AddTransient<IValidator<ExpenditureParameters>, ExpenditureParametersValidator>()
            .AddTransient<IValidator<ExpenditureNationalAvgParameters>, ExpenditureNationalAvgParametersValidator>()
            .AddTransient<IValidator<ExpenditureQueryParameters>, ExpenditureQueryParametersValidator>()
            .AddSingleton<IItSpendingService, ItSpendingService>()
            .AddSingleton<IIncomeService, IncomeService>()
            .AddSingleton<IBalanceService, BalanceService>()
            .AddSingleton<IExpenditureService, ExpenditureService>();

        return serviceCollection;
    }
}