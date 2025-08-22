using Draftli.Api.Hubs;
using Draftli.Infra.Redis;
using Draftli.Shared.Interfaces;
using StackExchange.Redis;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSignalR();

        builder.Services.AddSingleton<IConnectionMultiplexer>(
            sp => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("redis") ??
                                                throw new InvalidOperationException("Redis connection string is not configured."))
        );

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        builder.Services.AddSingleton<IDocumentRepository, RedisDocumentRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        app.MapHub<DocumentHub>("/hubs/document");

        app.Run();
    }
}