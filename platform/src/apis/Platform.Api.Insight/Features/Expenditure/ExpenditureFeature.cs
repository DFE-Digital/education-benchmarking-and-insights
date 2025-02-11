using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Api.Insight.Features.Expenditure.Validators;

namespace Platform.Api.Insight.Features.Expenditure;

[ExcludeFromCodeCoverage]
public static class ExpenditureFeature
{
    public static IServiceCollection AddExpenditureFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IExpenditureService, ExpenditureService>()
            .AddTransient<IValidator<ExpenditureParameters>, ExpenditureParametersValidator>()
            .AddTransient<IValidator<ExpenditureNationalAvgParameters>, ExpenditureNationalAvgParametersValidator>()
            .AddTransient<IValidator<ExpenditureQuerySchoolParameters>, ExpenditureQuerySchoolParametersValidator>()
            .AddTransient<IValidator<ExpenditureQueryTrustParameters>, ExpenditureQueryTrustParametersValidator>();

        return serviceCollection;
    }
}