using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Api.Establishment.Features.Schools.Validators;
using Platform.Search;

namespace Platform.Api.Establishment.Features.Schools;

[ExcludeFromCodeCoverage]
public static class SchoolsFeature
{
    public static IServiceCollection AddSchoolsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ISchoolComparatorsService, SchoolComparatorsService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, SchoolsSearchValidator>(nameof(SchoolsFeature));

        return serviceCollection;
    }
}