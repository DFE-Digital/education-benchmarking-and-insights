using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Api.School.Features.Census.Validators;
using Platform.Functions;

namespace Platform.Api.School.Features.Census;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddCensusFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IQueryHandler, QueryV1Handler>()
            .AddSingleton<IGetUserDefinedHandler, GetUserDefinedV1Handler>()
            .AddSingleton<IGetNationalAverageHistoryHandler, GetNationalAverageHistoryV1Handler>()
            .AddSingleton<IGetHistoryHandler, GetHistoryV1Handler>()
            .AddSingleton<IGetHandler, GetV1Handler>()
            .AddSingleton<IGetComparatorSetAverageHistoryHandler, GetComparatorSetAverageHistoryV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryHandler>, VersionedHandlerDispatcher<IQueryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetUserDefinedHandler>, VersionedHandlerDispatcher<IGetUserDefinedHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetNationalAverageHistoryHandler>, VersionedHandlerDispatcher<IGetNationalAverageHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetHistoryHandler>, VersionedHandlerDispatcher<IGetHistoryHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetHandler>, VersionedHandlerDispatcher<IGetHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetComparatorSetAverageHistoryHandler>, VersionedHandlerDispatcher<IGetComparatorSetAverageHistoryHandler>>()
            .AddTransient<IValidator<GetParameters>, GetParametersValidator>()
            .AddTransient<IValidator<QueryCensusParameters>, QueryParametersValidator>()
            .AddTransient<IValidator<NationalAvgParameters>, NationalAvgParametersValidator>()
            .AddSingleton<ICensusService, CensusService>();

        return serviceCollection;
    }
}