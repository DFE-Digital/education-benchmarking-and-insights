using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Validators;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddEducationHealthCarePlansFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IQueryEducationHealthCarePlansHandler, QueryEducationHealthCarePlansV1Handler>()
            .AddSingleton<IQueryEducationHealthCarePlansHistoryHandler, QueryEducationHealthCarePlansHistoryV1Handler>()
            .AddSingleton<IEducationHealthCarePlansService, EducationHealthCarePlansService>()
            .AddTransient<IValidator<EducationHealthCarePlansParameters>, EducationHealthCarePlansParametersValidator>();

        return serviceCollection;
    }
}