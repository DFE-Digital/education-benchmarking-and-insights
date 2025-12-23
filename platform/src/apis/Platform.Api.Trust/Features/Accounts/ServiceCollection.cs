using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Api.Trust.Features.Accounts.Validators;
using Platform.Functions;

namespace Platform.Api.Trust.Features.Accounts;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddAccountsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetIncomeHistoryHandler, GetIncomeHistoryV1Handler>()
            .AddSingleton<IGetBalanceHandler, GetBalanceV1Handler>()
            .AddSingleton<IGetBalanceHistoryHandler, GetBalanceHistoryV1Handler>()
            .AddSingleton<IQueryBalanceHandler, QueryBalanceV1Handler>()
            .AddSingleton<IGetExpenditureHandler, GetExpenditureV1Handler>()
            .AddSingleton<IGetExpenditureHistoryHandler, GetExpenditureHistoryV1Handler>()
            .AddSingleton<IQueryExpenditureHandler, QueryExpenditureV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetIncomeHistoryHandler>, VersionedHandlerDispatcher<IGetIncomeHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetBalanceHandler>, VersionedHandlerDispatcher<IGetBalanceHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetBalanceHistoryHandler>, VersionedHandlerDispatcher<IGetBalanceHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryBalanceHandler>, VersionedHandlerDispatcher<IQueryBalanceHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureHandler>, VersionedHandlerDispatcher<IGetExpenditureHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetExpenditureHistoryHandler>, VersionedHandlerDispatcher<IGetExpenditureHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryExpenditureHandler>, VersionedHandlerDispatcher<IQueryExpenditureHandler>>()
            .AddSingleton<IAccountsService, AccountsService>()
            .AddTransient<IValidator<IncomeParameters>, IncomeParametersValidator>()
            .AddTransient<IValidator<ExpenditureParameters>, ExpenditureParametersValidator>()
            .AddTransient<IValidator<ExpenditureQueryParameters>, ExpenditureQueryParametersValidator>();

        return serviceCollection;
    }
}