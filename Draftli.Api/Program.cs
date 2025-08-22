using Draftli.Api.Hubs;
using Draftli.Application.Handlers;
using Draftli.Infra.Redis;
using Draftli.Shared.Interfaces;
using StackExchange.Redis;

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
        {
            cfg.RegisterServicesFromAssemblyContaining<UpdateDocumentContentHandler>();
        });

        builder.Services.AddSingleton<IDocumentRepository, RedisDocumentRepository>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
        builder.Services.AddControllers();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReactApp", policy =>
            {
                policy.WithOrigins(builder.Configuration["CORS_ALLOWED_ORIGINS"] ??
                    throw new InvalidOperationException("CORS_ALLOWED_ORIGINS is not configured."))
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        app.UseCors("AllowReactApp");
        app.MapHub<DocumentHub>("/hubs/document");

        app.Run();
    }
}