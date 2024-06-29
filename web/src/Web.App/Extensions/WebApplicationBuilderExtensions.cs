using System.Reflection;
using Microsoft.OpenApi.Models;
using Web.App.Infrastructure.Cosmos;
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
                    var settings = section.GetSection("Settings").Get<Settings>();

                    ArgumentNullException.ThrowIfNull(settings);
                    ArgumentNullException.ThrowIfNull(settings.ContainerName);
                    ArgumentNullException.ThrowIfNull(settings.DatabaseName);

                    opts.ContainerName = settings.ContainerName;
                    opts.DatabaseName = settings.DatabaseName;
                    opts.CosmosClient = ClientFactory.Create(settings);
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
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return builder;
    }

    public static void AddSwaggerService(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddSwaggerGen(options =>
            {
                options.DocInclusionPredicate((_, apiDesc) =>
                    apiDesc.ActionDescriptor.DisplayName?.StartsWith("Web.App.Controllers.Api.") == true);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web.App API",

                    // lazy hack to add sign in/out links to UI
                    Contact = new OpenApiContact
                    {
                        Name = "🔒 Sign in",
                        Url = new Uri("/sign-in", UriKind.Relative)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "🔓 Sign out",
                        Url = new Uri("/sign-out", UriKind.Relative)
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
    }
}