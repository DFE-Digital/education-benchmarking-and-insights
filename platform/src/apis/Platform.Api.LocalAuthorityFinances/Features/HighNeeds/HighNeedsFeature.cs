﻿using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Validators;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds;

[ExcludeFromCodeCoverage]
public static class HighNeedsFeature
{
    public static IServiceCollection AddHighNeedsFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IHighNeedsService, HighNeedsStubService>();

        serviceCollection
            .AddTransient<IValidator<HighNeedsParameters>, HighNeedsParametersValidator>();

        return serviceCollection;
    }
}