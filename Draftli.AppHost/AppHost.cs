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

        builder.AddViteApp("react", "../Draftli.Web")
            .WithReference(apiService)
            .WaitFor(apiService)
            .WithNpmPackageInstallation();

        builder.Build().Run();
    }
}