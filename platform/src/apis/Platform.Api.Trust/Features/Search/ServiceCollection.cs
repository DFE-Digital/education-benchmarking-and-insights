using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Trust.Features.Search.Handlers;
using Platform.Api.Trust.Features.Search.Services;
using Platform.Api.Trust.Features.Search.Validators;
using Platform.Functions;
using Platform.Search;

namespace Platform.Api.Trust.Features.Search;

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
            .AddSingleton<ITrustSearchService, TrustSearchService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, TrustsSearchValidator>(Constants.Features.Search);

        return serviceCollection;
    }
}