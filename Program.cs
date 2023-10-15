using Microsoft.EntityFrameworkCore;
using SchoolHubApi;
using SchoolHubApi.Data;
using SchoolHubApi.Repositories.Implementation;
using SchoolHubApi.Repositories.Interface;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}