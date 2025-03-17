using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities.Parameters;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Api.Establishment.Features.LocalAuthorities.Validators;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

[ExcludeFromCodeCoverage]
public static class LocalAuthoritiesFeature
{
    public static IServiceCollection AddLocalAuthoritiesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>()
            .AddSingleton<ILocalAuthorityRankingService, LocalAuthorityRankingService>();

        serviceCollection
            .AddTransient<IValidator<LocalAuthoritiesNationalRankParameters>, LocalAuthoritiesNationalRankParametersValidator>();

        return serviceCollection;
    }
}