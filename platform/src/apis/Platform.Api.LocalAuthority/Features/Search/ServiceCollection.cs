using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Search.Handlers;
using Platform.Api.LocalAuthority.Features.Search.Services;
using Platform.Api.LocalAuthority.Features.Search.Validators;
using Platform.Functions;
using Platform.Search;

namespace Platform.Api.LocalAuthority.Features.Search;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddSearchFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IPostSearchHandler, PostSearchV1Handler>()
            .AddSingleton<IPostSuggestHandler, PostSuggestV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IPostSearchHandler>, VersionedHandlerDispatcher<IPostSearchHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IPostSuggestHandler>, VersionedHandlerDispatcher<IPostSuggestHandler>>()
            .AddSingleton<ILocalAuthoritySearchService, LocalAuthoritySearchService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, LocalAuthoritiesSearchValidator>(Constants.Features.Search);

        return serviceCollection;
    }
}