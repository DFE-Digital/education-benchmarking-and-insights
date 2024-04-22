using Carter;
using Platform.Api.Insight.Features.ComparatorSets;
using Platform.Api.Insight.Features.FinancialPlans;
using Platform.Api.Insight.Features.Ratings;
using Platform.Api.Insight.Features.SchoolFinances;
using Platform.Api.Insight.Features.Schools;
using Platform.Api.Insight.Features.TrustFinances;
using Platform.Api.Insight.Features.Workforce;
using Platform.Api.Insight.Middleware;
using Platform.Infrastructure.Cosmos;
using Platform.Infrastructure.Sql;
using Serilog;

namespace Platform.Api.Insight;

public static class Configuration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
        
        builder.Services
            .AddEndpointsApiExplorer()
            .AddCarter()
            .AddSwaggerGen()
            .AddHealthChecks();
        
        builder.Services.AddOptions<SqlDatabaseOptions>()
            .BindConfiguration("Sql")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        builder.Services.AddOptions<DbOptions>()
            .BindConfiguration("Database")
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        builder.Services.AddOptions<CosmosDatabaseOptions>()
            .BindConfiguration("Cosmos")
            .ValidateDataAnnotations()
            .ValidateOnStart();
   
        builder.Services.AddSingleton<IDatabaseFactory, DatabaseFactory>()
            .AddSingleton<IFinancialPlansDb, FinancialPlansDb>()
            .AddSingleton<IComparatorSetsDb, ComparatorSetsDb>()
            .AddSingleton<IWorkforceDb, WorkforceDb>()
            .AddSingleton<IRatingsDb, RatingsDb>()
            .AddSingleton<ITrustFinancesDb, TrustFinancesDb>()
            .AddSingleton<ISchoolFinancesDb, SchoolFinancesDb>()
            .AddSingleton<ISchoolsDb, SchoolsDb>()
            .AddSingleton<ICosmosClientFactory, CosmosClientFactory>();
    }

    public static void RegisterMiddlewares(this WebApplication app)
    {
        app.UseSwagger()
            .UseSwaggerUI()
            .UseHttpsRedirection()
            .UseExceptionHandleMiddleware();

        app.MapCarter();
        app.MapHealthChecks("/health");
    }
}