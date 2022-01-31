global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using MyCosts.API.Data;
global using MyCosts.API.Data.Models;
global using MyCosts.API.Data.Repositories.Products;
global using MyCosts.API.Data.Repositories.ProductCategories;
global using MyCosts.API.Models;
global using MyCosts.API.Services.Products;
global using MyCosts.API.Services.ProductCategories;
global using MyCosts.API.Utils;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.Text.Json.Serialization;

namespace MyCosts.API;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}