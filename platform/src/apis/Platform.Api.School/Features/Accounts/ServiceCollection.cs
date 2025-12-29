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
            .AddSingleton<IVersionedHandlerDispatcher<IQueryItSpendingHandler>, VersionedHandlerDispatcher<IQueryItSpendingHandler>>()
            .AddTransient<IValidator<ItSpendingParameters>, ItSpendingParametersValidator>()
            .AddSingleton<IItSpendingService, ItSpendingService>();

        return serviceCollection;
    }
}