
public static class IServiceCollectionExtensions
{

    public static void AddServices(this.IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
    }
}