using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Validators;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans;

[ExcludeFromCodeCoverage]
public static class EducationHealthCarePlansFeature
{
    public static IServiceCollection AddEducationHealthCarePlansFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IEducationHealthCarePlansService, EducationHealthCarePlansStubService>();

        serviceCollection
            .AddTransient<IValidator<EducationHealthCarePlansParameters>, EducationHealthCarePlansParametersValidator>();

        return serviceCollection;
    }
}