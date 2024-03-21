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
                    ArgumentNullException.ThrowIfNull(settings.ContainerName);
                    ArgumentNullException.ThrowIfNull(settings.DatabaseName);

                    opts.ContainerName = settings.ContainerName;
                    opts.DatabaseName = settings.DatabaseName;
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