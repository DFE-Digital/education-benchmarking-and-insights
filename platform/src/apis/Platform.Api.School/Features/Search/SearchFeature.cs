﻿using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Search.Handlers;
using Platform.Api.School.Features.Search.Services;
using Platform.Api.School.Features.Search.Validators;
using Platform.Functions;
using Platform.Search;

namespace Platform.Api.School.Features.Search;

[ExcludeFromCodeCoverage]
public static class SearchFeature
{
    public static IServiceCollection AddSearchFeature(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<IPostSearchHandler, PostSearchV1Handler>()
            .AddSingleton<IPostSuggestHandler, PostSuggestV1Handler>()
            .AddSingleton<IVersionedHandlerDispatcher<IPostSearchHandler>, VersionedHandlerDispatcher<IPostSearchHandler>>()
            .AddSingleton<IVersionedHandlerDispatcher<IPostSuggestHandler>, VersionedHandlerDispatcher<IPostSuggestHandler>>()
            .AddSingleton<ISchoolSearchService, SchoolSearchService>();

        serviceCollection
            .AddKeyedTransient<IValidator<SearchRequest>, SchoolsSearchValidator>(nameof(SearchFeature));

        return serviceCollection;
    }
}