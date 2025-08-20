internal class Program
{
    private static void Main(string[] args)
    {
        IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

        IResourceBuilder<ProjectResource> apiService = builder.AddProject<Projects.Draftli_Api>("draftli-api");

        builder.AddProject<Projects.Draftli_Worker>("draftli-worker");

        builder.AddViteApp("react", "../Draftli.Web")
            .WithReference(apiService)
            .WaitFor(apiService)
            .WithNpmPackageInstallation();

        builder.Build().Run();
    }
}