using Web.App.Infrastructure.Session;

namespace Web.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddCacheService(this WebApplicationBuilder builder)
    {
        var section = builder.Configuration.GetSection("SessionData");
        var storeType = section.GetValue<string>("Using");
        
        switch (storeType?.ToLower())
        {
            case "cosmos":
                builder.Services.AddCosmosCache(opts =>
                {
                    var settings = section.GetSection("Settings").Get<CosmosCacheSettings>();
                    ArgumentNullException.ThrowIfNull(settings);

                    opts.ContainerName = settings.ContainerName ?? throw new ArgumentNullException(nameof(settings.ContainerName));
                    opts.DatabaseName = settings.DatabaseName ?? throw new ArgumentNullException(nameof(settings.DatabaseName));
                    opts.CosmosClient = CosmosClientFactory.Create(settings);
                    opts.CreateIfNotExists = false;
                });
                break;
            default:
                builder.Services.AddDistributedMemoryCache();
                break;
        }
        
        return builder;
    }
}