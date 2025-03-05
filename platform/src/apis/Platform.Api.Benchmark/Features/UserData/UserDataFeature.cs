using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Services;
using Platform.Api.Benchmark.Features.UserData.Validators;

namespace Platform.Api.Benchmark.Features.UserData;

[ExcludeFromCodeCoverage]
public static class UserDataFeature
{
    public static IServiceCollection AddUserDataFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IUserDataService, UserDataService>()
            .AddTransient<IValidator<UserDataParameters>, UserDataParametersValidator>();

        return serviceCollection;
    }
}