using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Api.Establishment.Features.LocalAuthorities.Validators;
using Platform.Search;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

[ExcludeFromCodeCoverage]
public static class LocalAuthoritiesFeature
{
    public static IServiceCollection AddLocalAuthoritiesFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, LocalAuthoritiesSearchValidator>(nameof(LocalAuthoritiesFeature));
        return serviceCollection;
    }
}