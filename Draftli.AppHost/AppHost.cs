internal class Program
{
    private static void Main(string[] args)
    {
        IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

        IResourceBuilder<RedisResource> redis = builder.AddRedis("redis");

        IResourceBuilder<ProjectResource> apiService = builder
            .AddProject<Projects.Draftli_Api>("draftli-api")
            .WithReference(redis);

        builder.AddProject<Projects.Draftli_Worker>("draftli-worker");

        IResourceBuilder<NodeAppResource> react = builder.AddViteApp("react", "../Draftli.Web")
            .WithReference(apiService)
            .WaitFor(apiService)
            .WithNpmPackageInstallation()
            .WithEnvironment("VITE_API_URL", apiService.GetEndpoint("https"));

        apiService.WithEnvironment("CORS_ALLOWED_ORIGINS", react.GetEndpoint("http"));

        builder.Build().Run();
    }
}