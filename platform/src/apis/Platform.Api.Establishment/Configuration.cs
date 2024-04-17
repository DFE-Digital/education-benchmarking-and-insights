using Carter;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Api.Establishment.Middleware;
using Platform.Api.Establishment.Search;
using Platform.Infrastructure.Cosmos;
using Serilog;

namespace Platform.Api.Establishment;

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
        
        builder.Services.AddOptions<SearchServiceOptions>()
            .BindConfiguration("Search")
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

        builder.Services
            .AddSingleton<ICosmosClientFactory, CosmosClientFactory>()
            .AddSingleton<ITrustsDb, TrustsDb>()
            .AddSingleton<ISchoolsDb, SchoolsDb>()
            .AddSingleton<ISearchService<SchoolResponseModel>, SchoolsSearchService>()
            .AddSingleton<ISearchService<TrustResponseModel>, TrustsSearchService>();
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