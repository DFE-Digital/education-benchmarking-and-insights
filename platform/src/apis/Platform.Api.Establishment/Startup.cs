using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.Establishment;
using Platform.Api.Establishment.Db;
using Platform.Api.Establishment.Search;
using Platform.Domain;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Cosmos;
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

        builder.Services.AddSerilogLoggerProvider(Constants.ApplicationName);
        builder.Services.AddHealthChecks();

        builder.Services.AddOptions<SqlDatabaseOptions>().BindConfiguration("Sql").ValidateDataAnnotations();
        builder.Services.AddOptions<SchoolDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<TrustDbOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();
        builder.Services.AddOptions<SearchServiceOptions>().BindConfiguration("Search").ValidateDataAnnotations();
        builder.Services.AddOptions<CosmosDatabaseOptions>().BindConfiguration("Cosmos").ValidateDataAnnotations();

        builder.Services.AddSingleton<ICosmosClientFactory, CosmosClientFactory>();
        builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>();

        builder.Services.AddSingleton<ISchoolDb, SchoolDb>();
        builder.Services.AddSingleton<ITrustDb, TrustDb>();
        builder.Services.AddSingleton<ILocalAuthorityDb, LocalAuthorityDb>();

        builder.Services.AddSingleton<ISearchService<SchoolResponseModel>, SchoolSearchService>();
        builder.Services.AddSingleton<ISearchService<TrustResponseModel>, TrustSearchService>();
        builder.Services.AddSingleton<ISearchService<LocalAuthorityResponseModel>, LocalAuthoritySearchService>();

        builder.Services.AddTransient<IValidator<PostSuggestRequestModel>, PostSuggestRequestValidator>();
    }
}