namespace MyCosts.API.Utils;

public static class MyCostsUtils
{
    public static void AddMyCosts(this IServiceCollection services, IConfiguration configuration)
    {
        string connection = configuration.GetConnectionString("Default");
        //services.AddDbContext<MyCostsDbContext>(options => options.UseSqlServer(connection));

        services.AddDbContext<MyCostsDbContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("Default"))
            .EnableSensitiveDataLogging());

        services.AddScoped<IProductsRepository, ProductsDbRepository>();
        services.AddScoped<IProductCategoriesRepository, ProductCategoriesDbRepository>();

        services.AddTransient<IProductsService, ProductsService>();
        services.AddTransient<IProductCategoriesService, ProductCategoriesService>();
    }
}
