using Platform.Api.Establishment;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterServices();

var app = builder.Build();
app.RegisterMiddlewares();
app.Run();