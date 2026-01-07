using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Accounts.Handlers;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Api.LocalAuthority.Features.Accounts.Validators;
using Platform.Functions;

namespace Platform.Api.LocalAuthority.Features.Accounts;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddAccountsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IQueryHighNeedsHandler, QueryHighNeedsV1Handler>()
            .AddSingleton<IQueryHighNeedsHistoryHandler, QueryHighNeedsHistoryV1Handler>()
            .AddSingleton<IHighNeedsService, HighNeedsService>()
            .AddTransient<IValidator<HighNeedsParameters>, HighNeedsParametersValidator>();

        return serviceCollection;
    }
}