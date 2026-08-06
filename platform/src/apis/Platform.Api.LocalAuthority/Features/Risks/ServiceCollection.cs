using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthority.Features.Risks.Handlers;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Api.LocalAuthority.Features.Risks.Services;
using Platform.Api.LocalAuthority.Features.Risks.Validators;

namespace Platform.Api.LocalAuthority.Features.Risks;

[ExcludeFromCodeCoverage]
public static class ServiceCollection
{
    public static IServiceCollection AddRisksFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IQueryPagedSchoolRisksHandler, QueryPagedSchoolRisksHandlerV1>()
            .AddSingleton<ISchoolRisksService, SchoolRisksService>()
            .AddTransient<IValidator<PagedSchoolRisksParameters>, PagedSchoolRisksParametersValidator>();

        return serviceCollection;
    }
}
