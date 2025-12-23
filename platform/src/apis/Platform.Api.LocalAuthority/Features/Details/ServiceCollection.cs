using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Api.LocalAuthority.Features.Details.Validators;
using Platform.Functions;

namespace Platform.Api.LocalAuthority.Features.Details;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddDetailsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IGetLocalAuthorityHandler, GetLocalAuthorityV1Handler>()
            .AddSingleton<IQueryLocalAuthoritiesHandler, QueryLocalAuthoritiesV1Handler>()
            .AddSingleton<IQueryMaintainedSchoolFinanceHandler, QueryMaintainedSchoolFinanceV1Handler>()
            .AddSingleton<IQueryMaintainedSchoolWorkforceHandler, QueryMaintainedSchoolWorkforceV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IGetLocalAuthorityHandler>, VersionedHandlerDispatcher<IGetLocalAuthorityHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryLocalAuthoritiesHandler>, VersionedHandlerDispatcher<IQueryLocalAuthoritiesHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryMaintainedSchoolFinanceHandler>, VersionedHandlerDispatcher<IQueryMaintainedSchoolFinanceHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IQueryMaintainedSchoolWorkforceHandler>, VersionedHandlerDispatcher<IQueryMaintainedSchoolWorkforceHandler>>()
            .AddSingleton<ILocalAuthorityDetailsService, LocalAuthorityDetailsService>()
            .AddSingleton<IMaintainedSchoolsService, MaintainedSchoolsService>()
            .AddTransient<IValidator<FinanceSummaryParameters>, FinanceSummaryParametersValidator>()
            .AddTransient<IValidator<WorkforceSummaryParameters>, WorkforceSummaryParametersValidator>(); ;

        return serviceCollection;
    }
}