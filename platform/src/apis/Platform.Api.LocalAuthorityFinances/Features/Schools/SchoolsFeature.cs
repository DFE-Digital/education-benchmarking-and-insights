using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Services;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Validators;

namespace Platform.Api.LocalAuthorityFinances.Features.Schools;

[ExcludeFromCodeCoverage]
public static class SchoolsFeature
{
    public static IServiceCollection AddSchoolsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ISchoolsService, SchoolsService>();

        serviceCollection
            .AddTransient<IValidator<FinanceSummaryParameters>, FinanceSummaryParametersValidator>()
            .AddTransient<IValidator<WorkforceSummaryParameters>, WorkforceSummaryParametersValidator>();

        return serviceCollection;
    }
}