using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RestaurantRaterAPI;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    // public void ConfigureServices(IServiceCollection services)
    //     {
    //         services.AddControllers();
    //         services.AddSwaggerGen(c =>
    //         {
    //             c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestaurantRaterAPI", Version = "v1" });
    //         });
    //         services.AddHttpsRedirection(options => options.HttpsPort = 443);

    //     }
    // public static IHostBuilder CreateHostBuilder(string[] args) =>
    //     Host.CreateDefaultBuilder(args)
    //         .ConfigureWebHostDefaults(webBuilder=>
    //         {
    //             webBuilder.UseStartup<StartupBase>();
    //             webBuilder.UseUrls("http://localhost:80", "https://localhost:443");
    //         });
}