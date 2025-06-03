using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Trusts.Handlers;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Api.Establishment.Features.Trusts.Validators;
using Platform.Functions;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Trusts;

[ExcludeFromCodeCoverage]
public static class TrustsFeature
{
    public static IServiceCollection AddTrustsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetTrustHandler, GetTrustV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetTrustHandler>, VersionedHandlerDispatcher<IGetTrustHandler>>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ITrustComparatorsService, TrustComparatorsService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, TrustsSearchValidator>(nameof(TrustsFeature));

        return serviceCollection;
    }
}