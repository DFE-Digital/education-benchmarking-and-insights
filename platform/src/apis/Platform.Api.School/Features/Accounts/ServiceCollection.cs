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
            .AddSingleton<IGetIncomeComparatorSetAverageHistoryHandler, GetIncomeComparatorSetAverageHistoryV1Handler>()
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