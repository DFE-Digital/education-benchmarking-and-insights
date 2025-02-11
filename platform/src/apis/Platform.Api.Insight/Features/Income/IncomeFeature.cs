using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Insight.Features.Income.Parameters;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Api.Insight.Features.Income.Validators;

namespace Platform.Api.Insight.Features.Income;

[ExcludeFromCodeCoverage]
public static class IncomeFeature
{
    public static IServiceCollection AddIncomeFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IIncomeService, IncomeService>()
            .AddTransient<IValidator<IncomeParameters>, IncomeParametersValidator>();

        return serviceCollection;
    }
}