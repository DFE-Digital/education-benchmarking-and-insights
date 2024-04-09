namespace Web.App.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddSessionService(this WebApplicationBuilder builder)
    {
        var section = builder.Configuration.GetSection("SessionData");
        var storeType = section.GetValue<string>("Using");

        switch (storeType?.ToLower())
        {
            case "cosmos":
                builder.Services.AddCosmosCache(opts =>
                {
                    var settings = section.GetSection("Settings").Get<Infrastructure.Cosmos.Settings>();

                    ArgumentNullException.ThrowIfNull(settings);
                    ArgumentNullException.ThrowIfNull(settings.ContainerName);
                    ArgumentNullException.ThrowIfNull(settings.DatabaseName);

                    opts.ContainerName = settings.ContainerName;
                    opts.DatabaseName = settings.DatabaseName;
                    opts.CosmosClient = Infrastructure.Cosmos.ClientFactory.Create(settings);
                    opts.CreateIfNotExists = false;
                });
                break;
            
            case "redis":
                builder.Services.AddStackExchangeRedisCache(opts =>
                {
                    var settings = section.GetSection("Settings").Get<Infrastructure.Redis.Settings>();
                    
                    ArgumentNullException.ThrowIfNull(settings);
                    
                    opts.Configuration = settings.ConnectionString;
                    opts.InstanceName = settings.InstanceName;
                });
                break;
            default:
                builder.Services.AddDistributedMemoryCache();
                break;
        }

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(3600);
            options.Cookie.IsEssential = true;
        });
        
        return builder;
    }
}