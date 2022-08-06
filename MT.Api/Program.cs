using System.Text.Json;
using HashidsNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MT.Api.Database;

namespace MT.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers().AddJsonOptions(jsonOptions =>
        {
            jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        #region Swagger

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Money Tracker Api",
                Description = "Web Api for Money Tracker App",
            });
        });

        #endregion

        #region CORS

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DevelopmentCorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });

        #endregion

        #region Database

        builder.Services.AddDbContext<MoneyTrackerDbContext>(option =>
        {
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging();
        }, ServiceLifetime.Transient);

        #endregion

        #region MVC

        builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

        #endregion

        #region Utils

        builder.Services.AddSingleton<IHashids>(_ => new Hashids("E546C8DF278CD5931069B522E695D4F2", 11));

        #endregion

        WebApplication app = builder.Build();

        app.UseCors("DevelopmentCorsPolicy");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Money Tracker Api");
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
