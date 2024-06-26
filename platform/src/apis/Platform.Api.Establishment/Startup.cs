﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Platform.Api.Establishment.Trusts;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;
using Platform.Infrastructure.Sql;

[assembly: WebJobsStartup(typeof(Startup))]

namespace Platform.Api.Establishment;

[ExcludeFromCodeCoverage]
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.AddCustomSwashBuckle(Assembly.GetExecutingAssembly());

        var sql = Environment.GetEnvironmentVariable("Sql__ConnectionString");
        ArgumentNullException.ThrowIfNull(sql);

        builder.Services
            .AddSerilogLoggerProvider(Constants.ApplicationName);

        builder.Services
            .AddHealthChecks()
            .AddSqlServer(sql);

        builder.Services
            .AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations();

        builder.Services
            .AddOptions<SearchServiceOptions>()
            .BindConfiguration("Search")
            .ValidateDataAnnotations();

        builder.Services
            .AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<ISchoolsService, SchoolsService>()
            .AddSingleton<ITrustsService, TrustsService>()
            .AddSingleton<ILocalAuthoritiesService, LocalAuthoritiesService>();

        builder.Services
            .AddTransient<IValidator<SuggestRequest>, PostSuggestRequestValidator>();
    }
}